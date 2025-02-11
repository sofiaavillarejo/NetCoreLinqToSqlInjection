using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        //metodos que va a tener la interface
        List<Doctor> GetDoctores();

        void InsertDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital);
        void DeleteDoctor(int idDoctor);
        //void UpdateDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital);
    }
}
