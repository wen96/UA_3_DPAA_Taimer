﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TaimerGUI {
    public partial class AAddUser : Form {

        AGestUser parentForm = null;

        public AAddUser() {
            InitializeComponent();
        }

        public void setParent(AGestUser form) {
            parentForm = form;
        }

        private void btCancel_Click(object sender, EventArgs e) {
            if (MessageBox.Show(
                    "¿Esta seguro de que desea descartar el usuario?",
                    "Descartar usuario",
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                if (parentForm != null) {
                    Hide();
                    parentForm.Show();

                    AdminForm parent = (AdminForm)this.MdiParent;
                    parent.positionChilds();
                }
            }
        }

        private void btCreate_Click(object sender, EventArgs e) {
            bool valid = true;

            // Nombre
            if (tbName.Text == "") {
                lbErrName.Visible = true;
                valid = false;
            } else {
                lbErrName.Visible = false;
            }

            // Email
            Regex emailRegex = new Regex("[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}");
            if (tbEmail.Text == "") {
                lbErrEmail.Visible = true;
                lbErrEmailBad.Visible = false;
                valid = false;
            } else if (!emailRegex.IsMatch(tbEmail.Text)) {
                lbErrEmail.Visible = false;
                lbErrEmailBad.Visible = true;
                valid = false;
            } else {
                lbErrEmail.Visible = false;
                lbErrEmailBad.Visible = false;
            }

            // DNI
            Regex dniRegex = new Regex("[0-9]{8}[A-Z]");
            if (tbDni.Text == "") {
                lbErrDni.Visible = true;
                lbErrDniBad.Visible = false;
                valid = false;
            } else if (!dniRegex.IsMatch(tbDni.Text)) {
                lbErrDni.Visible = false;
                lbErrDniBad.Visible = true;
                valid = false;
            } else {
                lbErrDniBad.Visible = false;
                lbErrDni.Visible = false;
            }

            // Titulacion
            if (tbTitu.Text == "") {
                lbErrTitu.Visible = true;
                valid = false;
            } else {
                lbErrTitu.Visible = false;
            }

            // Contrasea
            if (tbPass.Text == "") {
                lbErrPass.Visible =true;
                valid = false;
            } else {
               lbErrPass.Visible = false;
            }


            if (parentForm != null && valid) {
                try
                {
                    Taimer.User nuevousuario = new Taimer.User(tbName.Text, tbDni.Text, tbEmail.Text,
                    tbPass.Text, (int)udCurso.Value, tbTitu.Text);
                    nuevousuario.Agregar();
                    Program.AddUsuario(nuevousuario);
                    parentForm.updateTable();


                    tbName.Text = "";
                    tbDni.Text = "";
                    tbEmail.Text = "";
                    tbPass.Text = "";
                    tbTitu.Text = "";
                    udCurso.Value = 1;

                    Hide();
                    parentForm.Show();

                    AdminForm parent = (AdminForm)this.MdiParent;
                    parent.positionChilds();
                }
                catch (SqlException ex)
                {
                    if(ex.Message.Contains("CAlt"))
                        MessageBox.Show("El email introducido ya existe. Introduce otro distinto");
                    else if (ex.Message.Contains("PRIMARY KEY"))
                        MessageBox.Show("El DNI introducido ya existe. Introduce otro distinto");
                    else
                        MessageBox.Show(ex.Message);
                }
                
                
            }

        }

    }
}
