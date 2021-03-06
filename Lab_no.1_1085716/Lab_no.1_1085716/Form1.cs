﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_no._1_1085716
{
    public partial class Form1 : Form
    {
        Dictionary<string, Cancion> bliblioteca = new Dictionary<string, Cancion>();
        List<Cancion> mylista = new List<Cancion>();
                        
        public Form1()
        {
            InitializeComponent();
            DataGridViewButtonColumn buttonColumn =
            new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "";
            buttonColumn.Name = "Play";
            buttonColumn.Text = "Play";
            buttonColumn.Width = 40;
            buttonColumn.UseColumnTextForButtonValue = true;
            DataGridViewButtonColumn agregfarLista =
            new DataGridViewButtonColumn();
            agregfarLista.HeaderText = "";
            agregfarLista.Name = "Agregar a lista";
            agregfarLista.Text = "Agregar a lista";
            agregfarLista.UseColumnTextForButtonValue = true;

            dgvInfo.Columns.Add(buttonColumn);
            dgvInfo.Columns.Add("Titulo", "Titulo");
            dgvInfo.Columns.Add("Artista", "Artista");
            dgvInfo.Columns.Add("Album", "Album");
            dgvInfo.Columns.Add("Genero", "Genero");
            dgvInfo.Columns.Add("Año", "Año");
            dgvInfo.Columns.Add(agregfarLista);

            DataGridViewButtonColumn buttonColumn2 =
            new DataGridViewButtonColumn();
            buttonColumn2.HeaderText = "";
            buttonColumn2.Name = "Play";
            buttonColumn2.Text = "Play";
            buttonColumn2.Width = 40;
            buttonColumn2.UseColumnTextForButtonValue = true;

            dgvLista.Columns.Add(buttonColumn2);
            dgvLista.Columns.Add("Titulo", "Titulo");
            dgvLista.Columns.Add("Artista", "Artista");
            dgvLista.Columns.Add("Album", "Album");
            dgvLista.Columns.Add("Genero", "Genero");
            dgvLista.Columns.Add("Año", "Año");


        }
       

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Mp3 files (*.mp3)|*.mp3";
            ofd.DefaultExt = ".mp3";
            if (DialogResult.OK == ofd.ShowDialog())
            {
                string[] name = ofd.FileNames;
                string[] path = ofd.SafeFileNames;

                
                for (int i = 0; i < name.Length-1; i++)
                {
                    TagLib.File file = TagLib.File.Create(name[i]);
                    Cancion cancion = new Cancion();
                    cancion.nombre = file.Tag.Title;
                    cancion.Artista = file.Tag.FirstArtist;
                    cancion.Album = file.Tag.Album;
                    cancion.Genero = file.Tag.FirstGenre;
                    cancion.año = Convert.ToInt32(file.Tag.Year);
                    cancion.ruta = name[i];
                    try
                    {
                        bliblioteca.Add(cancion.nombre, cancion);
                        dgvInfo.Rows.Add("", cancion.nombre, cancion.Artista, cancion.Album, cancion.Genero, cancion.año);
                    }
                    catch (Exception )
                    {

                        MessageBox.Show("EL archvio no contenia todos los parametros");
                    }
                    
                    
                   
                }
               
            }
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        { 
            
            try
            {
                IOrderedEnumerable<KeyValuePair<string, Cancion>> items=null;
                if (cbxTipoOrdenar.Text== "Ascendentes")
                {
                    if (cbxOrdenarpor.Text == "Titulo")
                    {
                         items = from pair in bliblioteca
                                    orderby pair.Value.nombre ascending
                                    select pair;
                    }
                    else if (cbxOrdenarpor.Text == "Artista")
                    {
                         items = from pair in bliblioteca
                                    orderby pair.Value.Artista ascending
                                    select pair;
                    }
                    else if (cbxOrdenarpor.Text == "Album")
                    {
                         items = from pair in bliblioteca
                                    orderby pair.Value.Album ascending
                                    select pair;
                    }
                    else if (cbxOrdenarpor.Text == "Genero")
                    {
                         items = from pair in bliblioteca
                                    orderby pair.Value.Genero ascending
                                    select pair;
                    }
                    else if (cbxOrdenarpor.Text == "Año")
                    {
                        items = from pair in bliblioteca
                                    orderby pair.Value.año ascending
                                    select pair;
                    }

                }
                else if(cbxTipoOrdenar.Text == "Descendentes")
                {
                    if (cbxOrdenarpor.Text == "Titulo")
                    {
                        items = from pair in bliblioteca
                                    orderby pair.Value.nombre descending
                                    select pair;
                    }
                    else if (cbxOrdenarpor.Text == "Artista")
                    {
                        items = from pair in bliblioteca
                                    orderby pair.Value.Artista descending
                                select pair;
                    }
                    else if (cbxOrdenarpor.Text == "Album")
                    {
                        items = from pair in bliblioteca
                                    orderby pair.Value.Album descending
                                select pair;
                    }
                    else if (cbxOrdenarpor.Text == "Genero")
                    {
                         items = from pair in bliblioteca
                                    orderby pair.Value.Genero descending
                                 select pair;
                    }
                    else if (cbxOrdenarpor.Text == "Año")
                    {
                         items = from pair in bliblioteca
                                    orderby pair.Value.año descending
                                 select pair;
                    }
                }

                dgvInfo.Rows.Clear();
                // Display results.
                foreach (KeyValuePair<string, Cancion> pair in items)
                {
                    Cancion cancion = pair.Value;
                    dgvInfo.Rows.Add("", cancion.nombre, cancion.Artista, cancion.Album, cancion.Genero, cancion.año);
                    

                }
            }
            catch
            {
                MessageBox.Show("No escogio tipo Ordenamiento");
            }
           

           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (bliblioteca.ContainsKey(textBox1.Text) == true)
                {
                    dgvInfo.Rows.Clear();

                    Cancion cancion = bliblioteca[textBox1.Text];
                    dgvInfo.Rows.Add("", cancion.nombre, cancion.Artista, cancion.Album, cancion.Genero, cancion.año);
                }
                else
                {
                    MessageBox.Show("No se encontro");
                }

            }
            catch (Exception)
            {


                MessageBox.Show("Error");
            }
            
            
        }

        private void dgvInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (dgvInfo.CurrentCell.ColumnIndex)
            {

                case 0:
                    {
                        int pocision = dgvInfo.CurrentCell.RowIndex;
                        string dreccion = dgvInfo.Rows[pocision].Cells[1].Value.ToString();
                        Cancion cancion = bliblioteca[dreccion];
                        axWindowsMediaPlayer1.URL = cancion.ruta;
                        axWindowsMediaPlayer1.Ctlcontrols.play();

                        break;
                    }

                case 6:
                    {
                        int pocision = dgvInfo.CurrentCell.RowIndex;
                        string dreccion = dgvInfo.Rows[pocision].Cells[1].Value.ToString();
                        Cancion cancion = bliblioteca[dreccion];
                        mylista.Add(cancion);
                        dgvLista.Rows.Add("", cancion.nombre, cancion.Artista, cancion.Album, cancion.Genero, cancion.año);
                        MessageBox.Show("Se agrego una cancion a la lista");

                        break;
                    }
            }
        }
    }
}
