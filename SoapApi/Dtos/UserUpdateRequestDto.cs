using System.Runtime.Serialization;
using System.Data;

namespace SoapApi.Dtos;
[DataContract]
public class UserUpdateRequestDto
{

    [DataMember]
    public string FirstName {get; set; } = null!;
    [DataMember]
    public string LastName {get; set; } = null!;
    [DataMember]
    public DateTime Birthday { get; set; }
}