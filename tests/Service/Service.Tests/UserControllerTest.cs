using AybCommerce.UI.Controllers;

namespace Service.Tests
{    
    public class UserControllerTest       
    {         
        readonly AddressServiceFake _addressServiceFake;             
        readonly UserServiceFake _userServiceFake;        
 
        UserController _controller;

        public UserControllerTest()
        {
            _addressServiceFake = new AddressServiceFake();
            // _controller = new UserController(_addressServiceFake, _userServiceFake);
        }
    }
}
 
 
