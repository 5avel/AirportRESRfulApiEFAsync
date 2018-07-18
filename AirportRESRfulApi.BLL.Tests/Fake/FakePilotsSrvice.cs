using System.Collections.Generic;
using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using System.Linq;

namespace AirportRESRfulApi.BLL.Tests.Fake
{
    public class FakePilotsSrvice : IPilotsService
    {
        public List<PilotDto> Entitys { get; set; }
        public FakePilotsSrvice()
        {
            Entitys = new List<PilotDto>();
        }

        public void Delete(int id)
        {
            var entitysForDeleting = Entitys.SingleOrDefault(x => x.Id == id);
            if (entitysForDeleting != null)
                Entitys.Remove(entitysForDeleting);
        }

        public IEnumerable<PilotDto> Get()
        {
            return Entitys;
        }

        public PilotDto GetById(int id)
        {
            return Entitys.SingleOrDefault(x => x.Id == id);
        }

        public void Make(PilotDto entity)
        {
            var entitysForCreate = Entitys.SingleOrDefault(x => x.Id == entity.Id);
            if (entitysForCreate == null)
                Entitys.Add(entity);
        }

        public void Update(PilotDto entity)
        {
            var entitysForUpdate = Entitys.SingleOrDefault(x => x.Id == entity.Id);
            if (entitysForUpdate != null)
            {
                Entitys.Remove(entitysForUpdate);
                Entitys.Add(entity);
            }

        }
    }
}
