﻿using System.Data;
using Microsoft.Data.SqlClient;
using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Repositories
{
    #region STORED PROCEDURES
    //create procedure SP_DELETE_DOCTOR(@iddoctor int)
    //as
    // delete from DOCTOR where DOCTOR_NO=@iddoctor
    //go
    //--------------------------------------
    //update
    //create procedure SP_UPDATE_DOCTOR(@idhospital int, @iddoctor int, @apellido nvarchar(50), @especialidad nvarchar(50), @salario int)
    //as
	   // update DOCTOR set HOSPITAL_COD = @idhospital, DOCTOR_NO = @iddoctor, APELLIDO = @apellido, ESPECIALIDAD = @especialidad, SALARIO = @salario
    //go

    #endregion

    public class RepositoryDoctoresSQLServer: IRepositoryDoctores //indicamos que heredamos de la interface creada -> para adaptarlo a oracle
    {
        private DataTable tableDoctores;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.tableDoctores = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter
                ("select * from DOCTOR", connectionString);
            ad.Fill(this.tableDoctores);
        }


        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.IdDoctor = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                doc.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doc);
            }
            return doctores;
        }

        public void InsertDoctor (int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values (@idhospital, @iddoctor "
                + ", @apellido, @especialidad, @salario)";
            this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            this.com.Parameters.AddWithValue("@idhospital", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
        public void DeleteDoctor(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";
            this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        //public void UpdateDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        //{
        //    string sql = "SP_UPDATE_DOCTOR";
        //    this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
        //    this.com.Parameters.AddWithValue("@apellido", apellido);
        //    this.com.Parameters.AddWithValue("@especialidad", especialidad);
        //    this.com.Parameters.AddWithValue("@salario", salario);
        //    this.com.Parameters.AddWithValue("@idhospital", idHospital); 
        //    this.com.CommandType = CommandType.StoredProcedure;
        //    this.com.CommandText = sql;
        //    this.cn.Open();
        //    this.com.ExecuteNonQuery();
        //    this.cn.Close();
        //    this.com.Parameters.Clear();
        //}
    }
}
