using AirCoil_API.Data;
using AirCoil_API.Models;

namespace AirCoil_API
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context) { this.dataContext = context; }

        public void SeedDataContext()
        {
            if (!dataContext.Provinces.Any())
            {
                var provinces = new List<Province>()
                {
                    new Province() { Name = "กรุงเทพมหานคร" },
                    new Province() { Name = "กระบี่" },
                    new Province() { Name = "กาญจนบุรี" },
                    new Province() { Name = "กาฬสินธุ์" },
                    new Province() { Name = "กำแพงเพชร" },
                    new Province() { Name = "ขอนแก่น" },
                    new Province() { Name = "จันทบุรี" },
                    new Province() { Name = "ฉะเชิงเทรา" },
                    new Province() { Name = "ชลบุรี" },
                    new Province() { Name = "ชัยนาท" },
                    new Province() { Name = "ชัยภูมิ" },
                    new Province() { Name = "ชุมพร" },
                    new Province() { Name = "เชียงใหม่" },
                    new Province() { Name = "เชียงราย" },
                    new Province() { Name = "ตรัง" },
                    new Province() { Name = "ตราด" },
                    new Province() { Name = "ตาก" },
                    new Province() { Name = "นครนายก" },
                    new Province() { Name = "นครปฐม" },
                    new Province() { Name = "นครพนม" },
                    new Province() { Name = "นครราชสีมา" },
                    new Province() { Name = "นครศรีธรรมราช" },
                    new Province() { Name = "นครสวรรค์" },
                    new Province() { Name = "นนทบุรี" },
                    new Province() { Name = "นราธิวาส" },
                    new Province() { Name = "น่าน" },
                    new Province() { Name = "บึงกาฬ" },
                    new Province() { Name = "บุรีรัมย์" },
                    new Province() { Name = "ปทุมธานี" },
                    new Province() { Name = "ประจวบคีรีขันธ์" },
                    new Province() { Name = "ปราจีนบุรี" },
                    new Province() { Name = "ปัตตานี" },
                    new Province() { Name = "พระนครศรีอยุธยา" },
                    new Province() { Name = "พังงา" },
                    new Province() { Name = "พัทลุง" },
                    new Province() { Name = "พิจิตร" },
                    new Province() { Name = "พิษณุโลก" },
                    new Province() { Name = "เพชรบุรี" },
                    new Province() { Name = "เพชรบูรณ์" },
                    new Province() { Name = "แพร่" },
                    new Province() { Name = "พะเยา" },
                    new Province() { Name = "ภูเก็ต" },
                    new Province() { Name = "มหาสารคาม" },
                    new Province() { Name = "มุกดาหาร" },
                    new Province() { Name = "แม่ฮ่องสอน" },
                    new Province() { Name = "ยโสธร" },
                    new Province() { Name = "ยะลา" },
                    new Province() { Name = "ร้อยเอ็ด" },
                    new Province() { Name = "ระนอง" },
                    new Province() { Name = "ระยอง" },
                    new Province() { Name = "ราชบุรี" },
                    new Province() { Name = "ลพบุรี" },
                    new Province() { Name = "ลำปาง" },
                    new Province() { Name = "ลำพูน" },
                    new Province() { Name = "เลย" },
                    new Province() { Name = "ศรีสะเกษ" },
                    new Province() { Name = "สกลนคร" },
                    new Province() { Name = "สงขลา" },
                    new Province() { Name = "สตูล" },
                    new Province() { Name = "สมุทรปราการ" },
                    new Province() { Name = "สมุทรสงคราม" },
                    new Province() { Name = "สมุทรสาคร" },
                    new Province() { Name = "สระแก้ว" },
                    new Province() { Name = "สระบุรี" },
                    new Province() { Name = "สิงห์บุรี" },
                    new Province() { Name = "สุโขทัย" },
                    new Province() { Name = "สุพรรณบุรี" },
                    new Province() { Name = "สุราษฎร์ธานี" },
                    new Province() { Name = "สุรินทร์" },
                    new Province() { Name = "หนองคาย" },
                    new Province() { Name = "หนองบัวลำภู" },
                    new Province() { Name = "อ่างทอง" },
                    new Province() { Name = "อำนาจเจริญ" },
                    new Province() { Name = "อุดรธานี" },
                    new Province() { Name = "อุตรดิตถ์" },
                    new Province() { Name = "อุทัยธานี" },
                    new Province() { Name = "อุบลราชธานี" }
                };

                dataContext.Provinces.AddRange(provinces);
                dataContext.SaveChanges();
            }

            if (!dataContext.Brands.Any())
            {
                dataContext.Brands.Add(new Brand() { Name = "Toyota" });
                dataContext.SaveChanges();
            }

            var toyota = dataContext.Brands.Where(b => b.Name == "Toyota").FirstOrDefault();

            if (!dataContext.Models.Any())
            {
                var models = new List<Model>()
                {
                    new Model()
                    {
                        Name = "86GT",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "ALPHARD",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "AVANZA",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "C-HR",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "CAMRY",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "COMMUTER",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "COROLLA ALTIS",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "CORONA",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "FORTUNER",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "HIACE",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "HILUX REVO DOUBLE CAB",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "HILUX REVO SMART CUB",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "HILUX REVO STANDARD CAB",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "HILUX VIGO",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "INNOVA CRYSTA",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "PRIUS",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "SIENTA",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "VELLFIRE",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "VENTURY",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "VIOS",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "WISH",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "YARIS",
                        Brand = toyota
                    },
                    new Model()
                    {
                        Name = "YARIS ATIV",
                        Brand = toyota
                    }
                };

                dataContext.Models.AddRange(models);
                dataContext.SaveChanges();
            }

            if (!dataContext.Cars.Any())
            {
                var cars = new List<Car>()
                {
                    new Car()
                    {
                        LicensePlate = "มก123",
                        CreatedAt = DateTime.Now,
                        Province = dataContext.Provinces.Where(p => p.Name == "นครราชสีมา").FirstOrDefault(),
                        Model = dataContext.Models.Where(m => m.Name == "YARIS").FirstOrDefault()
                    }
                };

                dataContext.Cars.AddRange(cars);
                dataContext.SaveChanges();
            }

            if (!dataContext.Results.Any())
            {
                var results = new List<Result>()
                {
                    new Result()
                    {
                        EDLLevel = 0,
                        Description = "รอการประมวลผล"
                    },
                    new Result()
                    {
                        EDLLevel = 1,
                        Description = "คอยล์สกปรกมาก มีผลต่อสุขภาพ ต้องล้างแอร์"
                    },
                    new Result()
                    {
                        EDLLevel = 2,
                        Description = "คอยล์สกปรกมาก ควรล้างแอร์รถยนต์"
                    },
                    new Result()
                    {
                        EDLLevel = 3,
                        Description = "คอยล์เย็นสกปรก พิจารนาการล้างคอยล์เย็น"
                    },
                    new Result()
                    {
                        EDLLevel = 4,
                        Description = "คอยล์เย็นเริ่มสกปรก ควรรักษาความสะอาดภายในรถ"
                    },
                    new Result()
                    {
                        EDLLevel = 5,
                        Description = "ยินดีด้วย คอยล์เย็นของคุณสะอาดสดชื่น"
                    },
                };

                dataContext.Results.AddRange(results);
                dataContext.SaveChanges();
            }

            if (!dataContext.Branches.Any()) 
            {
                var branches = new List<Branch>()
                {
                    new Branch()
                    {
                        Name = "ดอนเมือง",
                        ServiceCenter = new ServiceCenter()
                        {
                            Name = "บางกอก"
                        }
                    }
                };

                dataContext.Branches.AddRange(branches);
                dataContext.SaveChanges();
            }
        }
    }
}
