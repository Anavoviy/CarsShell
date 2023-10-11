using Cars.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Services
{
    public class DataBase
    {
        private int autoIncrementCars = 0;
        private int autoIncrementEngines = 0;
        private int autoIncrementBodyTypes = 0;
        private int autoIncrementUsers = 0;

        List<Car> Cars = new List<Car>();
        List<Engine> Engines = new List<Engine>();
        List<BodyType> BodyTypes = new List<BodyType>();
        List<User> Users = new List<User>();

        private int AuthorizationUserId = 0;

        public DataBase()
        {
            
        }

        //Users
        public async Task<User> GetUserAsync(int id)
        {
            User user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return null;
            return user;
        }
        public async Task<bool> AddUserAsync(User user)
        {
            if (user == null)
                return false;

            user.Id = ++autoIncrementUsers;

            Users.Add(user);

            return true;
        }


        //Cars
        public async Task<List<Car>> GetCarsAsync()
        {
            if(AuthorizationUserId == 0)
                return Cars;
            else
            {
                return Cars.Where(c => c.UserId == AuthorizationUserId).ToList();
            }
        }
        public async Task<Car> GetCarAsync(int id)
        {
            Car car = Cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return null;
            return car;
        }

        public async Task<bool> AddCarAsync(Car car)
        {
            if(car == null)
                return false;

            string engineView = "";
            string bodyTypeView = "";

            Engine engine = DB.instance.GetEngineAsync(car.IdEngine).Result;
            engineView += ("Двигатель: " + engine.Model + "; " + engine.CylinderArrangement + " " + engine.CylinderCapacity);
            
            BodyType bodyType = DB.instance.GetBodyTypeAsync(car.IdBodyType).Result;
            bodyTypeView += ("Кузов: " + bodyType.Title);
            
            car.EngineView = engineView;
            car.BodyTypeView = bodyTypeView;
            car.Id = ++autoIncrementCars;

            Cars.Add(car);

            return true;
        }
        public async Task<bool> UpdateCarAsync(Car car)
        {
            if (car == null || Cars.FirstOrDefault(c => c.Id == car.Id) == null)
                return false;

            Car oldCar = Cars.FirstOrDefault(c => c.Id == car.Id);
            oldCar.Model = car.Model;
            oldCar.Description = car.Description;
            oldCar.IdEngine = car.IdEngine;
            oldCar.IdBodyType = car.IdBodyType;

            Engine engine = DB.instance.GetEngineAsync(car.IdEngine).Result;
            BodyType bodyType = DB.instance.GetBodyTypeAsync(car.IdBodyType).Result;

            oldCar.EngineView = ("Двигатель: " + engine.Model + "; " + engine.CylinderArrangement + " " + engine.CylinderCapacity);
            oldCar.BodyTypeView = ("Кузов: " + bodyType.Title);

            return true;
        }
        public async Task<bool> DeleteCarAsync(Car car)
        {
            if(car == null || Cars.FirstOrDefault(c => c.Id == car.Id) == null)
                return false;

            Cars.Remove(car);

            return true;
        }

        //Engines
        public async Task<List<Engine>> GetEnginesAsync()
        {
            if (AuthorizationUserId == 0)
                return Engines;
            else
            {
                return Engines.Where(c => c.UserId == AuthorizationUserId).ToList();
            }
        }
        public async Task<Engine> GetEngineAsync(int id)
        {
            Engine engine = Engines.FirstOrDefault(e => e.Id == id);
            if (engine == null)
                return null;
            return engine;
        }

        public async Task<bool> AddEngineAsync(Engine engine)
        {
            if (engine == null)
                return false;

            engine.Id = ++autoIncrementEngines;

            Engines.Add(engine);

            return true;
        }
        public async Task<bool> UpdateEngineAsync(Engine engine)
        {
            if (engine == null || Engines.FirstOrDefault(e => e.Id == engine.Id) == null)
                return false;

            Engine oldEngine = Engines.FirstOrDefault(e => e.Id == engine.Id);
            oldEngine.Model = engine.Model;
            oldEngine.CylinderCapacity = engine.CylinderCapacity;
            oldEngine.CylinderArrangement = engine.CylinderArrangement;
            oldEngine.HorsePower = engine.HorsePower;

            return true;
        }
        public async Task<bool> DeleteEngineAsync(Engine engine)
        {
            if (engine == null || Engines.FirstOrDefault(e => e.Id == engine.Id) == null)
                return false;

            Engines.Remove(engine);

            return true;
        }

        //BodyTypes
        public async Task<List<BodyType>> GetBodyTypesAsync()
        {
            if (AuthorizationUserId == 0)
                return BodyTypes;
            else
            {
                return BodyTypes.Where(c => c.UserId == AuthorizationUserId).ToList();
            }
        }
        public async Task<BodyType> GetBodyTypeAsync(int id)
        {
            BodyType bodyType = BodyTypes.FirstOrDefault(b => b.Id == id);
            if (bodyType == null)
                return null;

            return bodyType;
        }

        public async Task<bool> AddBodyTypeAsync(BodyType bodyType)
        {
            if (bodyType == null)
                return false;

            bodyType.Id = ++autoIncrementBodyTypes;

            BodyTypes.Add(bodyType);

            return true;
        }
        public async Task<bool> UpdateBodyTypeAsync(BodyType bodyType)
        {
            if (bodyType == null || BodyTypes.FirstOrDefault(b => b.Id == bodyType.Id) == null)
                return false;

            BodyType oldBodyType = BodyTypes.FirstOrDefault(b => b.Id == bodyType.Id);
            oldBodyType.Title = bodyType.Title;

            return true;
        }
        public async Task<bool> DeleteBodyTypeAsync(BodyType bodyType)
        {
            if (bodyType == null || BodyTypes.FirstOrDefault(b => b.Id == bodyType.Id) == null)
                return false;

            BodyTypes.Remove(bodyType);

            return true;
        }


        //Authorization
        public async Task<string> Authorization(string login, string password)
        {
            if (Users.FirstOrDefault(u => u.Login == login && u.Password == password) != null)
            {
                AuthorizationUserId = Users.FirstOrDefault(u => u.Login == login && u.Password == password).Id;

                return "";
            }
            else
                return "Неправильно введён логин или пароль";
        }

        public async Task<string> RegisterUser(string login, string password)
        {
            if (login == password)
                return "Логин и пароль не должны быть одинаковыми";

            DB.instance.AddUserAsync(new User { Login = login, Password = password });
            return "";
        }
    } 
}
