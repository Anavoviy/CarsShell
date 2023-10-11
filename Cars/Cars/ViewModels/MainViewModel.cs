using Cars.Models;
using Cars.Services;
using Cars.Views;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using BodyType = Cars.Models.BodyType;

namespace Cars.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Car> cars;
        private ObservableCollection<BodyType> bodyTypes;
        private ObservableCollection<Engine> engines;

        public ObservableCollection<Car> Cars { get => cars; set { cars = value; Signal(); } }
        public ObservableCollection<BodyType> BodyTypes { get => bodyTypes; set { bodyTypes = value; Signal(); } }
        public ObservableCollection<Engine> Engines { get => engines; set { engines = value; Signal(); } }

        public Car SelectedCar { get; set; }
        public Engine SelectedEngine { get; set; }
        public BodyType SelectedBodyType { get; set; }



        public BaseCommand AddCar { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync($"EditCar?id={0}");
        });
        public BaseCommand AddEngine { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync($"EditEngine?id={0}");
        });
        public BaseCommand AddBodyType { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync($"EditBodyType?id={0}");
        });

        public static BaseCommandParameter DeleteCar { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            if (arg != null && arg as Car != null)
            {
                Car car = (Car)arg;
                DB.instance.DeleteCarAsync(car);
            }
            AddCarViewModel.Exit.Execute();
        });
        public static BaseCommandParameter DeleteEngine { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            if (arg != null && arg as Engine != null)
            {
                Engine engine = (Engine)arg;
                DB.instance.DeleteEngineAsync(engine);
            }
            AddEngineViewModel.Exit.Execute();
        });
        public static BaseCommandParameter DeleteBodyType { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            if(arg != null && arg as BodyType != null)
            {
                BodyType bodyType = (BodyType)arg;
                DB.instance.DeleteBodyTypeAsync(bodyType);
            }
            AddBodyTypeViewModel.Exit.Execute();
        });

        public MainViewModel() 
        {
            if(DB.instance.GetCarsAsync().Result.Count == 0 && DB.instance.GetBodyTypesAsync().Result.Count == 0 && DB.instance.GetEnginesAsync().Result.Count == 0)

            Cars = new ObservableCollection<Car>(DB.instance.GetCarsAsync().Result.ToList());
            BodyTypes = new ObservableCollection<BodyType>(DB.instance.GetBodyTypesAsync().Result.ToList());
            Engines = new ObservableCollection<Engine>(DB.instance.GetEnginesAsync().Result.ToList());

            Routing.RegisterRoute("EditCar", typeof(AddCarPage));
            Routing.RegisterRoute("EditEngine", typeof(AddEnginePage));
            Routing.RegisterRoute("EditBodyType", typeof(AddBodyTypePage));

            Signal();
        }

        private async void AddDataInDB()
        {
            await DB.instance.AddUserAsync(new User()
            {
                Login = "admin",
                Password = "admin"
            });


            await DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Джип 5дв.",
                UserId = 1
            });
            await DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Седан",
                UserId = 1
            });
            await DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Хэтчбэк 5дв.",
                UserId = 1
            });
            await DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Пикап",
                UserId = 1
            });
            
            await DB.instance.AddEngineAsync(new Engine()
            {
                Model = "J20A",
                HorsePower = 140,
                CylinderArrangement = "I4",
                CylinderCapacity = 2.0,

                UserId = 1
            });
            await DB.instance.AddEngineAsync(new Engine()
            {
                Model = "2JZGE",
                HorsePower = 135,
                CylinderArrangement = "I4",
                CylinderCapacity = 2.0,

                UserId = 1
            });
            await DB.instance.AddEngineAsync(new Engine()
            {
                Model = "L15C",
                HorsePower = 182,
                CylinderArrangement = "I4",
                CylinderCapacity = 1.5,

                UserId = 1
            });
            await DB.instance.AddEngineAsync(new Engine()
            {
                Model = "CUMMINS 6.7L",
                HorsePower = 370,
                CylinderArrangement = "I6",
                CylinderCapacity = 6.7,

                UserId = 1
            });
            await DB.instance.AddEngineAsync(new Engine()
            {
                Model = "4D20",
                HorsePower = 163,
                CylinderArrangement = "I4",
                CylinderCapacity = 2.0,

                UserId = 1
            });
             
            await DB.instance.AddCarAsync(new Car()
            {
                Model = "Suzuki Grand Vitara",
                IdBodyType = 1,
                IdEngine = 1,
                Description = "2007 год",

                UserId = 1
            });
            await DB.instance.AddCarAsync(new Car()
            {
                Model = "Toyota Crown",
                IdBodyType = 2,
                IdEngine = 2,
                Description = "1989 год",

                UserId = 1
            });
            await DB.instance.AddCarAsync(new Car()
            {
                Model = "Honda Civic",
                IdBodyType = 3,
                IdEngine = 3,
                Description = "2018 год",

                UserId = 1
            });
            await DB.instance.AddCarAsync(new Car()
            {
                Model = "Dodge Ram",
                IdBodyType = 4,
                IdEngine = 4,
                Description = "2014 год",

                UserId = 1
            });
            await DB.instance.AddCarAsync(new Car()
            {
                Model = "Great Wall Poer KingKong",
                IdBodyType = 4,
                IdEngine = 5,
                Description = "2023 год",

                UserId = 1
            });
        }
    }
}
