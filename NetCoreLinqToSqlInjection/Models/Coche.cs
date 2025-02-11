﻿using System.Security.Cryptography.X509Certificates;

namespace NetCoreLinqToSqlInjection.Models
{
    public class Coche : ICoche
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }
        public Coche()
        {
            this.Marca = "Utilitario";
            this.Modelo = "Utilitiario";
            this.Imagen = "coche1.jpg";
            this.Velocidad = 0;
            this.VelocidadMaxima = 120;
        }
        public void Acelerar()
        {
            this.Velocidad += 10;

            if(this.Velocidad >= this.VelocidadMaxima)
            {
                this.Velocidad = this.VelocidadMaxima;
            }
        }
        public void Frenar()
        {
            this.Velocidad -= 10;

            if (this.Velocidad < 0)
            {
                this.Velocidad = 0;
            }
        }
    }
}
