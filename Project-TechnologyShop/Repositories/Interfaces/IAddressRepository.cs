using Shop.Models;

namespace Shop.Repositories.Interfaces;
public interface IAddressRepository
{   
    Task<List<Address>> GetAddresses();
    bool AddNewAddress(Address address);
    bool RemoveAddress( int id);
}