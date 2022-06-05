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
namespace EFEFP1Av4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refrescar();
        }

        private void Refrescar()
        {
            using (programacion1Entities db = new programacion1Entities())
            {
                var lst = from d in db.tb_Inventario
                          select d;
                dataGridView1.DataSource = lst.ToList();
            }
        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            Servicios.FormServicios ofrmTabla = new Servicios.FormServicios();
            ofrmTabla.ShowDialog();
            Refrescar();
        }

        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }
        private void buttonModificar_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                Servicios.FormServicios ofrmTabla = new Servicios.FormServicios(id);
                ofrmTabla.ShowDialog();
                Refrescar();
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            MsEliminar();
        }
        private void MsEliminar()
        {
            DialogResult r = MessageBox.Show("¿Estas seguro que quieres Eliminar el producto?", "Eliminar", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                int? id = GetId();
                if (id != null)
                {
                    using (programacion1Entities db = new programacion1Entities())
                    {
                        tb_Inventario oTabla = db.tb_Inventario.Find(id);
                        db.tb_Inventario.Remove(oTabla);
                        db.SaveChanges();
                    }
                    MessageBox.Show("Se eliminó exitosamente");
                    Refrescar();
                }
            }
        }
    }
}
