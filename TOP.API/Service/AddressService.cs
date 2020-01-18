using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.API.Data.Context;
using TOP.API.Data.Helpers;
using TOP.Library.Data.models;

namespace TOP.API.Service
{
    public class AddressService : IModelService<Address>
    {
        readonly TOPContext _topContext;

        public AddressService(TOPContext topContext)
        {
            _topContext = topContext;
        }
        public Address Add(Address addressParam)
        {
            Address address = new Address();
            address = _topContext.Addresses.FirstOrDefault(x => x.address == addressParam.address);

            if (address != null)
                return null;

            _topContext.Addresses.Add(addressParam);
            _topContext.SaveChanges();
            return addressParam;
        }

        public Address Get(Guid addressId)
        {
            Address address = new Address();
            address = _topContext.Addresses.FirstOrDefault(x => x.Id == addressId);

            if (address == null)
                return null;

            return address;
        }

        public Address GetByName(string name)
        {
            Address address = new Address();
            address = _topContext.Addresses.FirstOrDefault(x => x.address == name);

            if (address == null)
                return null;

            return address;
        }

        public void Delete(Address address)
        {
            _topContext.Addresses.Remove(address);
            _topContext.SaveChanges();
        }

        public void Update(Address address)
        {
            Address dbAddress = new Address();
            dbAddress = _topContext.Addresses.FirstOrDefault(x => x.Id == address.Id);
            dbAddress.address = address.address;
            _topContext.SaveChanges();
        }
        public IEnumerable<Address> GetAll()
        {
            return null;
        }
    }
}
