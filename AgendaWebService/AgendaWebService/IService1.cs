using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AgendaWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Login(string username, string password);

        [OperationContract]
        void UpdateItemDetails(string notes, string time, string itemName);

        [OperationContract]
        void UpdateMeetingTime(string time, string meetingName);

        [OperationContract]
        string[] LoadMeetings();

        [OperationContract]
        string[] LoadMeetingDetails(string selected);

        [OperationContract]
        string[] LoadItems(string selected);

        [OperationContract]
        string[] LoadItemDetails(string selected);



        // TODO: Add your service operations here
    }


    
}
