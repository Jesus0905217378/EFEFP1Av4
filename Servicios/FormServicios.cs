using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFEFP1Av4.Models;

namespace EFEFP1Av4.Servicios
{
    public partial class FormServicios : Form
    {
        public int? id;
        tb_Inventario oTabla = null;
        public FormServicios(int? id=null)
        {
            InitializeComponent();
            this.id = id;
            if (id != null)
            {
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            using (programacion1Entities db = new programacion1Entities())
            {
                oTabla = db.tb_Inventario.Find(id);
                textBoxSKU.Text = Convert.ToString(oTabla.SKU);
                textBoxProducto.Text = oTabla.Producto;
                textBoxPCosto.Text = Convert.ToString(oTabla.PrecioCosto);
                textBoxPPublico.Text = Convert.ToString(oTabla.PrecioPublico);
                textBoxPMayorista.Text = Convert.ToString(oTabla.PrecioMayorista);
                dateTimePicker1.Value = (DateTime)oTabla.FechaIngreso;
                textBoxAlmacen.Text=Convert.ToString(oTabla.Almacen);


            }
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            using (programacion1Entities db = new programacion1Entities())
            {
                if(id == null)
                oTabla = new tb_Inventario();

                oTabla.SKU = Convert.ToInt32(textBoxSKU.Text);
                oTabla.Producto = textBoxProducto.Text;
                oTabla.PrecioCosto= Convert.ToInt32(textBoxPCosto.Text);
                oTabla.PrecioPublico = Convert.ToInt32(textBoxPPublico.Text);
                oTabla.PrecioMayorista = Convert.ToInt32(textBoxPMayorista.Text);
                oTabla.FechaIngreso = dateTimePicker1.Value;
                oTabla.Almacen = Convert.ToInt32(textBoxAlmacen.Text);

                if(id== null)
                db.tb_Inventario.Add(oTabla);
                else
                {
                    db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                MessageBox.Show("Se ingresó correctamente");
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Estas seguro que quieres Cancelar la operación?", "Cancelar", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
            //if (r == DialogResult.No)
            //{

            //}
        }
    }
}
