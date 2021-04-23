using Models;
using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class ClientLogic
    {
        readonly IRepoBase<GymClient> clientRepo;
        //IRepoBase<WorkoutDetail> detailRepo;
        //IRepoBase<ExtraInfo> infoRepo;

        public ClientLogic(IRepoBase<GymClient> clientRepo)
        {
            this.clientRepo = clientRepo;
        }

        #region CRUD methods
        public void AddClient(GymClient gymClient)
        {
            this.clientRepo.Add(gymClient);
        }

        public void DeleteClient(string clientId)
        {
            this.clientRepo.Delete(clientId);
        }

        public IQueryable<GymClient> GetClients()
        {
            return clientRepo.Read();
        }

        public GymClient GetClient(string clientId)
        {
            return clientRepo.GetItem(clientId);
        }

        public void UpdateClient(string clientId, GymClient newClient)
        {
            clientRepo.Update(clientId, newClient);
        }

        #endregion

        #region Non-CRUD methods
        public string LongestInfo()
        {
            int counter = 0;
            string helper = "";

            var something = GetClients().ToList();
            foreach(var item in something)
            {
                foreach(var info in item.ExtraInfos)
                {
                    if (info.Information.Length > counter)
                    {
                        counter = info.Information.Length;
                        helper = info.Information;
                    }
                }   
            }

            return helper;
        }
        public int AmountOfAlcoholists()
        {
            int counter = 0;
            var something = GetClients().ToList();
            foreach (var item in something)
            {
                foreach(var infos in item.ExtraInfos)
                {
                    if (infos.Information.ToLower() == "alkoholista" ||
                        infos.Information.ToLower() == "alkoholist" ||
                        infos.Information.ToLower() == "alcoholist" ||
                        infos.Information.ToLower() == "alcoholista" ||
                        infos.Information.ToLower() == "alcoholic")
                        counter++;
                }
            }
            return counter;
        }

        public void AddInfoToClient(ExtraInfo extraInfo, string clientId)
        {
            extraInfo.GymID = clientId;
            GetClient(clientId).ExtraInfos.Add(extraInfo);

            clientRepo.Save();
        }

        public void RemoveInfoFromClient(ExtraInfo extraInfo, string clientId)
        {
            GetClient(clientId).ExtraInfos.Remove(extraInfo);
            clientRepo.Save();
        }

        public void Valamiidkyet()
        {

            //var query = from x in detailRepo.Read()
            //            join y in infoRepo.Read() on x.GymClient equals y.GymClient
            //            group x by x.GymClient.FullName into g
            //            select g.Key;
        }

        public void FillDbWithSamples()
        {
            //WorkoutDetail_v2 d1 = new WorkoutDetail_v2()
            //{
            //    ContestDiets = ContestDiets.lowCarb,
            //    WorkoutType = WorkoutTypes.calisthenics,
            //    GymID = null
            //};
            //WorkoutDetail_v2 d2 = new WorkoutDetail_v2()
            //{
            //    ContestDiets = ContestDiets.intermittentFasting,
            //    WorkoutType = WorkoutTypes.powerlifting,
            //    GymID = null
            //};
            //WorkoutDetail_v2 d3 = new WorkoutDetail_v2()
            //{
            //    ContestDiets = ContestDiets.carbCycling,
            //    WorkoutType = WorkoutTypes.calisthenics,
            //    GymID = null
            //};

            ExtraInfo i0 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Hardcore wowos" };
            ExtraInfo i1 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Nem szeret lábazni" };
            ExtraInfo i2 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Váll problémái vannak" };
            ExtraInfo i3 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Nem szeret élni" };
            ExtraInfo i4 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Depressziós" };
            ExtraInfo i5 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Heti 9szer eddz" };
            ExtraInfo i6 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Heti 3szer eddz" };
            ExtraInfo i7 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Heti 2szer eddz" };
            ExtraInfo i8 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Heti 8szer eddz" };
            ExtraInfo i9 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Heti 6szer eddz" };
            ExtraInfo i10 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Heti 16szer eddz" };
            ExtraInfo i11 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Egyáltalán ezt valaki elolvassa?" };
            ExtraInfo i12 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Can we hit 9000 likes on dis vidio gujz" };
            ExtraInfo i13 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Megcsalta a feleségét" };
            ExtraInfo i14 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Alkoholista" };
            ExtraInfo i15 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Csukló problémái vannak" };
            ExtraInfo i16 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Alcoholist" };
            ExtraInfo i17 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Alcoholic" };
            ExtraInfo i18 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Ez az info csak azért lett létrehozva hogy bekerüljön a leghosszabb infohoz, Hello =)"};

            //AddDetailToClient(d1,"test01");
            //AddDetailToClient(d2,"test02");
            //AddDetailToClient(d3,"test03");

            AddInfoToClient(i0, "test01");
            AddInfoToClient(i1, "test01");
            AddInfoToClient(i2, "test02");
            AddInfoToClient(i3, "test03");
            AddInfoToClient(i4, "test04");
            AddInfoToClient(i5, "test05");
            AddInfoToClient(i6, "test04");
            AddInfoToClient(i7, "test03");
            AddInfoToClient(i8, "test02");
            AddInfoToClient(i9, "test01");
            AddInfoToClient(i10, "test06");
            AddInfoToClient(i11, "test07");
            AddInfoToClient(i12, "test08");
            AddInfoToClient(i13, "test09");
            AddInfoToClient(i14, "test10");
            AddInfoToClient(i15, "test07");
            AddInfoToClient(i16, "test08");
            AddInfoToClient(i17, "test05");
            AddInfoToClient(i18, "test10");
        }
        #endregion

    }
}
