using System.Collections.Generic;

namespace CityParkWeb.Entities.Utils
{
    public  class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

    }
}
