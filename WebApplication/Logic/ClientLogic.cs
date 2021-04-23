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
        IRepoBase<ExtraInfo> infoRepo;

        public ClientLogic(IRepoBase<GymClient> clientRepo, IRepoBase<ExtraInfo> infoRepo)
        {
            this.clientRepo = clientRepo;
            this.infoRepo = infoRepo;
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
            infoRepo.Delete(extraInfo.InfoId);
            clientRepo.Save();
        }

        public void FillDbWithSamples()
        {
            ExtraInfo i0 = new ExtraInfo() { Information = "Hardcore wowos" };
            ExtraInfo i1 = new ExtraInfo() { Information = "Nem szeret lábazni" };
            ExtraInfo i2 = new ExtraInfo() { Information = "Váll problémái vannak" };
            ExtraInfo i3 = new ExtraInfo() { Information = "Nem szeret élni" };
            ExtraInfo i4 = new ExtraInfo() { Information = "Depressziós" };
            ExtraInfo i5 = new ExtraInfo() { Information = "Heti 9szer eddz" };
            ExtraInfo i6 = new ExtraInfo() { Information = "Heti 3szer eddz" };
            ExtraInfo i7 = new ExtraInfo() { Information = "Heti 2szer eddz" };
            ExtraInfo i8 = new ExtraInfo() { Information = "Heti 8szer eddz" };
            ExtraInfo i9 = new ExtraInfo() { Information = "Heti 6szer eddz" };
            ExtraInfo i10 = new ExtraInfo() { Information = "Heti 16szer eddz" };
            ExtraInfo i11 = new ExtraInfo() { Information = "Egyáltalán ezt valaki elolvassa?" };
            ExtraInfo i12 = new ExtraInfo() { Information = "Can we hit 9000 likes on dis vidio gujz" };
            ExtraInfo i13 = new ExtraInfo() { Information = "Megcsalta a feleségét" };
            ExtraInfo i14 = new ExtraInfo() { Information = "Alkoholista" };
            ExtraInfo i15 = new ExtraInfo() { Information = "Csukló problémái vannak" };
            ExtraInfo i16 = new ExtraInfo() { Information = "Alcoholist" };
            ExtraInfo i17 = new ExtraInfo() { Information = "Alcoholic" };
            ExtraInfo i18 = new ExtraInfo() { Information = "Ez az info csak azért lett létrehozva hogy bekerüljön a leghosszabb infohoz, Hello =)"};

            IList<GymClient> collection = this.GetClients().ToList();

            AddInfoToClient(i0, collection[0].GymID);
            AddInfoToClient(i1, collection[1].GymID);
            AddInfoToClient(i2, collection[2].GymID);
            AddInfoToClient(i3, collection[3].GymID);
            AddInfoToClient(i4, collection[4].GymID);
            AddInfoToClient(i5, collection[5].GymID);
            AddInfoToClient(i6, collection[4].GymID);
            AddInfoToClient(i7, collection[3].GymID);
            AddInfoToClient(i8, collection[2].GymID);
            AddInfoToClient(i9, collection[1].GymID);
            AddInfoToClient(i10, collection[6].GymID);
            AddInfoToClient(i11, collection[7].GymID);
            AddInfoToClient(i12, collection[8].GymID);
            AddInfoToClient(i13, collection[9].GymID);
            AddInfoToClient(i14, collection[10].GymID);
            AddInfoToClient(i15, collection[7].GymID);
            AddInfoToClient(i16, collection[8].GymID);
            AddInfoToClient(i17, collection[5].GymID);
            AddInfoToClient(i18, collection[10].GymID);
        }
        #endregion

    }
}
