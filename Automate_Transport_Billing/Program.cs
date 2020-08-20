using System;

namespace Automate_Transport_Billing
{
    abstract class Vehicle
    {
        protected int billID;
        protected char fuelType;
        protected string vehicleMake;
        protected string vehicleType;
        protected int noOfKiloMeters;
        protected float ratePerKiloMeter;
        protected static int counter = 1001;

        public Vehicle(char fuelType, string vehicleMake, string vehicleType, int noOfKiloMeters)
        {
            this.fuelType = fuelType;
            this.vehicleMake = vehicleMake;
            this.vehicleType = vehicleType;
            this.noOfKiloMeters = noOfKiloMeters;
        }

        public int getBillID()
        {
            billID = counter++;
            return billID;
        }

        public char getFuelType()
        {
            return fuelType;
        }

        public string getVehicleMake()
        {
            return vehicleMake;
        }

        public string getVehicleType()
        {
            return vehicleType;
        }

        public int getNoOfKiloMeters()
        {
            return noOfKiloMeters;
        }

        public float getRatePerKiloMeter()
        {
            return ratePerKiloMeter;
        }

        public void validateFuelType()
        {
            if(fuelType != 'P' && fuelType != 'D')
            {
                fuelType = 'D';
                Console.WriteLine("Invalid fuel type, set the value to D");
            }
        }

        public abstract void calculateRatePerKiloMeter();

        public abstract double calculateBill();

    }
    class MiniVehicle : Vehicle
    {
        private int seatingCapacity;

        public MiniVehicle(char fuelType, string vehicleMake, string vehicleType, int noOfKiloMeters, int seatingCapacity) 
            : base(fuelType,vehicleMake,vehicleType, noOfKiloMeters)
        {
            this.seatingCapacity = seatingCapacity;
        }

        public int getSeatingCapacity()
        {
            return seatingCapacity;
        }

        public override void calculateRatePerKiloMeter()
        {
            ratePerKiloMeter = 4.5f + (seatingCapacity - 4);
        }

        public override double calculateBill()
        {
            return noOfKiloMeters * ratePerKiloMeter;
        }
    }
    class MaxiVehicle : Vehicle
    {
        private float loadInKG;
        private float ratePerKG;

        public MaxiVehicle(char fuelType, string vehicleMake, string vehicleType, int noOfKiloMeters, float loadInKG)
            : base(fuelType, vehicleMake, vehicleType, noOfKiloMeters)
        {
            this.loadInKG = loadInKG;
        }

        public float getLoadInKG()
        {
            return loadInKG;
        }

        public float getRatePerKG()
        {
            return ratePerKG;
        }

        public bool validateLoadInKG()
        {
            if (loadInKG >= 100 && loadInKG <= 5000)
                return true;
            else
                return false;
        }

        public void calculateRatePerKG()
        {
            if (vehicleMake.Equals("Ashok LeyLand", StringComparison.InvariantCultureIgnoreCase))
                ratePerKG = 1.5f;
            else if (vehicleMake.Equals("Mahindra", StringComparison.InvariantCultureIgnoreCase))
                ratePerKG = 1.0f;
            else
                ratePerKG = 0.5f;
        }

        public override void calculateRatePerKiloMeter()
        {
            if (fuelType == 'P')
                ratePerKiloMeter = 6f;
            else
                ratePerKiloMeter = 5f;
        }

        public override double calculateBill()
        {
            if(!validateLoadInKG())
            {
                Console.WriteLine("Unable to calculate the bill as the entered loadInKG is incorrect");
                return 0.00d;
            }

            return noOfKiloMeters * ratePerKiloMeter + loadInKG * ratePerKG;
        }
    }
    class Demo
    {
        static void Main(string[] args)
        {
            getInitializeObjMini("Passenger");
            getInitializeObjMaxi("Load");
        }

        static MiniVehicle getInitializeObjMini(string vehicleType)
        {
            MiniVehicle mini = new MiniVehicle('P', "Mazda", vehicleType, 40, 7);
            Console.WriteLine("---- Mini Vechile---");
            mini.validateFuelType();
            mini.calculateRatePerKiloMeter();
            Console.WriteLine("{0,-20} : {1}", "Vehicle Type", mini.getVehicleType());
            Console.WriteLine("{0,-20} : {1}", "BillID", mini.getBillID());
            Console.WriteLine("{0,-20} : {1:0.00}", "Rate per Kilo Meter", mini.getRatePerKiloMeter());
            Console.WriteLine("{0,-20} : {1}", "Fuel Type", mini.getFuelType());
            Console.WriteLine("{0,-20} : {1}", "Seating Capacity", mini.getSeatingCapacity());
            Console.WriteLine("{0,-20} : {1:0.00}", "Bill Amount",mini.calculateBill());
            return mini;
        }

        static MaxiVehicle getInitializeObjMaxi(string vehicleType )
        {
            MaxiVehicle maxi =  new MaxiVehicle('D', "Tata", vehicleType, 200, 1500);
            Console.WriteLine("\n---- Maxi Vechile---");
            maxi.validateFuelType();
            maxi.calculateRatePerKiloMeter();
            maxi.calculateRatePerKG();
            Console.WriteLine("{0,-20} : {1}", "Vehicle Type", maxi.getVehicleType());
            Console.WriteLine("{0,-20} : {1}", "BillID", maxi.getBillID());
            Console.WriteLine("{0,-20} : {1:0.00}", "Rate per Kilo Meter",  maxi.getRatePerKiloMeter());
            Console.WriteLine("{0,-20} : {1}", "Fuel Type",  maxi.getFuelType());
            Console.WriteLine("{0,-20} : {1:0.000}", "Load In KG",  maxi.getLoadInKG());
            Console.WriteLine("{0,-20} : {1:0.00}", "Rate per KG",  maxi.getRatePerKG());
            Console.WriteLine("{0,-20} : {1:0.00}", "Bill Amount", maxi.calculateBill());
            return maxi;    
        }
    }
}
