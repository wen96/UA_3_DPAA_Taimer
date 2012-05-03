﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taimer;
using System.IO;

namespace WebTaimer
{
    public partial class NoLogin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void botonIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string user = textboxUsuario.Text;
                string pass = textboxPassword.Text;
                if (user != "" && pass != "")
                {
                    Session.Add("usuario", User.CheckLoginUser(user, pass));
                    Response.Redirect("~/TabInicio/ConLogin.aspx");
                }
                else
                    throw new InvalidDataException();
                
                
            }
            catch (InvalidDataException)
            {
                Response.Write("<script>alert(\"usuario o contraseña incorrectos!\")</script>");
                // TODO: Mostrar mensaje de error de usuario o contraseña
            }
            
        }
    }
}