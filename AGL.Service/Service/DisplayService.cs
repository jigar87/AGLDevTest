using AGL.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AGL.Service.Service
{
    public class DisplayService
    {
        //HttpClient is intended to be instantiated once for life of an application.
        private static HttpClient Client = new HttpClient();        
        public async Task<string> GetAPIData(string apiUrl)
        {
            try
            {
                using (var response = await Client.GetAsync(new Uri(apiUrl)))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            catch(Exception ex)
            {
                //TODO: Add log4net error logging.
                throw ex;
            }
        }

        private List<Owner> GetOwnerAndPetData(string apiUrl)
        {
            var apiData = GetAPIData(apiUrl).Result;
            var ownerList = JsonConvert.DeserializeObject<List<Owner>>(apiData);

            return ownerList;
        }

        public bool VerifyOwnersGender(string apiUrl, string petName, string ownersGender)
        {
            var ownerList = GetOwnerAndPetData(apiUrl);
            var petOwnerExists = (from o in ownerList
                                   where o.Pets != null &&
                                   o.Gender == ownersGender &&
                                   o.Pets.Any(p => p.Name == petName)
                                   select o).Any();

            return petOwnerExists;

        }
        

        public List<Cats> GetCatsGroupedByOwnerGender(string apiUrl)
        {
            try
            {
                var ownerList = GetOwnerAndPetData(apiUrl);
                var obj = ownerList.GroupBy(oc => oc.Gender).ToList();
                var query = (from owner in ownerList
                             where owner.Pets != null
                             group owner by new { owner.Gender } into grp
                             select new Cats
                             {
                                 Gender = grp.Key.Gender,
                                 CatNames = (grp.SelectMany(ow => ow.Pets).Where(p => p.Type == "Cat").
                                            OrderBy(x => x.Name).
                                            Select(y => y.Name)).ToList()
                             }).ToList();

                return query;
            }
            catch(Exception ex)
            {
                //TODO: Add log4net error logging.
                throw ex;
            }

        }

    }    
}
