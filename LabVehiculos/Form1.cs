using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace LabVehiculos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            label7.Visible = false;
        }
        List<Clientes> clientes = new List<Clientes>();
        List<Vehiculos> vehiculos = new List<Vehiculos>();
        List<Alquileres> alquileres = new List<Alquileres>();
        List<Reporte> reportes = new List<Reporte>();
        private void Guardar(string fileName, string texto)
        {
           
            FileStream flujo = new FileStream("Vehiculos.txt", FileMode.Open, FileAccess.Write);
            
            StreamWriter writer = new StreamWriter(flujo);
            
            foreach (var vehiculo in vehiculos)
            {
                writer.WriteLine(vehiculo.Placa);
                writer.WriteLine(vehiculo.Marca);
                writer.WriteLine(vehiculo.Modelo);
                writer.WriteLine(vehiculo.Color);
                writer.WriteLine(vehiculo.Precio);

            }

            
            writer.Close();


        }
        private void GuardarVehiculo()
        {
            
            FileStream flujo = new FileStream("Vehiculos.txt", FileMode.Open, FileAccess.Write);
            
            StreamWriter writer = new StreamWriter(flujo);
            
            foreach (var vehiculo in vehiculos)
            {
                writer.WriteLine(vehiculo.Placa);
                writer.WriteLine(vehiculo.Marca);
                writer.WriteLine(vehiculo.Modelo);
                writer.WriteLine(vehiculo.Color);
                writer.WriteLine(vehiculo.Precio);

            }
 
            writer.Close();


        }
        private void GuardarReporte()
        {
           
            FileStream flujo = new FileStream("Reporte.txt", FileMode.Open, FileAccess.Write);
            
            StreamWriter writer = new StreamWriter(flujo);
            
            foreach (var reporte in reportes)
            {
                writer.WriteLine(reporte.Nombre);
                writer.WriteLine(reporte.Marca);
                writer.WriteLine(reporte.Modelo1);
                writer.WriteLine(reporte.FechaDevo);
                writer.WriteLine(reporte.Total);

            }

            
            writer.Close();


        }

      
        
        private void button1_Click(object sender, EventArgs e)
        {
            MostrarAlquileres();
            MostrarClientes();

        }
        public void CargarClientes()
        {
            
            string fileName = "Clientes.txt";

            
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            
            while (reader.Peek() > -1)
            
            {

                Clientes cliente = new Clientes();
                cliente.Nit = Convert.ToInt32(reader.ReadLine());
                cliente.Nombre = (reader.ReadLine());
                cliente.Address = (reader.ReadLine());


                
                clientes.Add(cliente);
            }
            
            reader.Close();
        }
        private void cargar_alquileres()
        {
            string fileName = "Alquileres.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                Alquileres alquiler = new Alquileres();
                alquiler.Nitp = int.Parse(reader.ReadLine());
                alquiler.Placa = reader.ReadLine();
                alquiler.Fechaalq = Convert.ToDateTime(reader.ReadLine());
                alquiler.Fechadev = Convert.ToDateTime(reader.ReadLine());
                alquiler.Km = int.Parse(reader.ReadLine());
                alquileres.Add(alquiler);
            }
            reader.Close();
        }

        public void MostrarClientes()
        {
            
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = clientes;
            dataGridView1.Refresh();
        }
        public void MostrarVehiculos()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = vehiculos;
            dataGridView2.Refresh();
        }
        public void MostrarAlquileres()
        {
            dataGridView4.DataSource = null;
            dataGridView4.DataSource = alquileres;
            dataGridView4.Refresh();
        }
        public void MostrarReporte()
        {
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = reportes;
            dataGridView3.Refresh();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            
            reportes.Clear();
            
            foreach (var alquiler in alquileres)
                {
                  
                  Clientes cliente = clientes.Find(c => c.Nit == alquiler.Nitp);
                    
                  
                  Vehiculos vehiculo = vehiculos.Find(v => v.Placa == alquiler.Placa);
                        

                            
                            Reporte reporte = new Reporte();
                            reporte.Nombre = cliente.Nombre;
                            reporte.Marca = vehiculo.Marca;
                            reporte.Modelo1 = vehiculo.Modelo;
                            reporte.FechaDevo = alquiler.Fechadev;
                            reporte.Total = vehiculo.Precio * alquiler.Km;

                            reportes.Add(reporte);
                }
                    
                
            
            MostrarReporte();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Vehiculos vehiculo = new Vehiculos();
            vehiculo.Placa = textBox1.Text;
            vehiculo.Marca = textBox2.Text;
            vehiculo.Modelo = textBox3.Text;
            vehiculo.Color = textBox4.Text;
            vehiculo.Precio = Convert.ToDecimal(textBox5.Text);

            vehiculos.Add(vehiculo);
            GuardarVehiculo();
            MostrarVehiculos();
            textBox1.Clear();
            textBox2.Clear();   
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int kmMayor = alquileres.Max(a => a.Km);
            label6.Text = "Vehiculo con Mayor Kilometraje:";
            label7.Text = kmMayor.ToString();
            label6.Visible = true;
            label7.Visible = true;
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            CargarClientes();
            cargar_alquileres();
        }
    }
}
