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
        readonly List<Address> _addresses = new List<Address>();//Azure testing

        public AddressService(TOPContext topContext)
        {
            _topContext = topContext;
            if (AppSettings.isAzure)
            {
                AddDefaults();
            }
        }

        private void AddDefaults()
        {
            Address address= new Address()
            {
                Id = Guid.Parse("125c5813-7542-4861-90d1-7ce9403fb4c4"),
                address = "test"
            };
            Address address2 = new Address()
            {
                Id = Guid.Parse("e6a34daa-e635-4ab6-bd90-991a78d73125"),
                address = "test2"
            };
            _addresses.Add(address);
            _addresses.Add(address2);
        }

        public Address Add(Address addressParam)
        {
            Address address = new Address();
            if (AppSettings.isAzure)
            {
                address = _addresses.FirstOrDefault(x => x.address == addressParam.address);

                if (address != null)
                    return null;


                addressParam.Id = Guid.NewGuid();
                _addresses.Add(addressParam);
                return addressParam;
            }
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
            if (AppSettings.isAzure)
            {
                address = _addresses.FirstOrDefault(x => x.Id == addressId);

                if (address == null)
                    return null;

                return address;
            }
            address = _topContext.Addresses.FirstOrDefault(x => x.Id == addressId);

            if (address == null)
                return null;

            return address;
        }

        public Address GetByName(string name)
        {
            Address address = new Address();
            if (AppSettings.isAzure)
            {
                address = _addresses.FirstOrDefault(x => x.address == name);

                if (address == null)
                    return null;

                return address;
            }
            address = _topContext.Addresses.FirstOrDefault(x => x.address == name);

            if (address == null)
                return null;

            return address;
        }

        public void Delete(Address address)
        {
            if (AppSettings.isAzure)
            {
                _addresses.Remove(address);
                return;
            }
            _topContext.Addresses.Remove(address);
            _topContext.SaveChanges();
        }

        public void Update(Address address)
        {
            Address dbAddress = new Address();
            if (AppSettings.isAzure)
            {
                dbAddress = _addresses.FirstOrDefault(x => x.Id == address.Id);
                dbAddress.address = address.address;
                return;
            }
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
