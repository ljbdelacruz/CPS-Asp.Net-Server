
using CentralProcessSystem.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Models.CPS
{
    #region ApplicationInformation
    public class ApplicationInformationVM {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        #region static methods
        public static ApplicationInformation set(Guid id, string name, string desc) {
            try {
                return new ApplicationInformation() {
                    ID = id,
                    Name = name,
                    Description = desc
                };
            } catch { return null; }
        }
        public static ApplicationInformationVM MToVM(ApplicationInformation model) {
            try {
                return new ApplicationInformationVM() {
                    ID = model.ID.ToString(),
                    Name = model.Name,
                    Description = model.Description
                };
            } catch { return null; }
        }
        public static List<ApplicationInformationVM> MsToVMs(List<ApplicationInformation> models) {
            var list = new List<ApplicationInformationVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class AdsInformationVM {
        public string ID { get; set; }
        public string AdInformation { get; set; }
        public string URL { get; set; }
        public StatusTypesReferencesVM Category { get; set; }
        public List<StatusTypesReferencesVM> AdCategory { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        public List<ImageLinkStorageVM> Images { get; set; }

        #region static methods
        public static AdsInformation set(Guid id, string ai, string url, Guid cid, Guid dtid) {
            try {
                return new AdsInformation() {
                    ID=id,
                    AdInformation=ai,
                    URL=url,
                    Category=cid,
                    DateTimeID=dtid
                };
            } catch { return null; }
        }
        public static AdsInformationVM MToVM(AdsInformation model) {
            try {
                return new AdsInformationVM(){
                    ID=model.ID.ToString(),
                    AdInformation=model.AdInformation,
                    URL=model.URL
                };
            } catch { return null; }
        }
        public static List<AdsInformationVM> MsToVMs(List<AdsInformation> models) {
            var list = new List<AdsInformationVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }



    #endregion
    #region Status Reference
    public class StatusTypesReferencesVM {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public StatusTypesReferences Category { get; set; }
        //if ownerID = ID of other status
        public List<StatusTypesReferencesVM> SubStatus { get; set; }
        public string OwnerID { get; set; }
        public string CategoryID { get; set; }
        public StatusTypesReferencesVM Category { get; set; }
        public string API { get; set; }
        #region static methods
        public static StatusTypesReferences set(Guid id, string name, string desc, Guid oid, Guid aid, Guid dtid, Guid catID) {
            try {
                return new StatusTypesReferences() {
                    ID = id,
                    Name = name,
                    Description = desc,
                    OwnerID=oid,
                    API=aid,
                    DateTimeStorageID =dtid,
                    CategoryID=catID
                };
            } catch { return null; }
        }
        public static StatusTypesReferencesVM MToVM(StatusTypesReferences model) {
            try {
                return new StatusTypesReferencesVM() {
                    ID=model.ID.ToString(),
                    Name = model.Name,
                    Description = model.Description
                };
            } catch { return null; }
        }
        public static List<StatusTypesReferencesVM> MsToVMs(List<StatusTypesReferences> models) {
            var list = new List<StatusTypesReferencesVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region user related information
    public class SignalRDataVM {
        public string ID { get; set; }
        public string SignalRID { get; set; }
        #region static methods
        public static SignalRData set(Guid id, Guid oid, Guid sid, Guid aid, Guid dtid, string hub, bool isActive) {
            try {
                return new SignalRData() {
                    ID=id,
                    OwnerID=oid,
                    SignalRID=sid,
                    API=aid,
                    DateTimeID=dtid,
                    isActive=isActive,
                    Hub=hub,
                };
            } catch { return null; }
        }
        public static SignalRDataVM MToVM(SignalRData model) {
            try {
                return new SignalRDataVM() {
                    ID=model.ID.ToString(),
                    SignalRID=model.SignalRID.ToString(),
                };
            } catch { return null; }
        }
        public static List<SignalRDataVM> MsToVMs(List<SignalRData> models) {
            var list = new List<SignalRDataVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class UserAccessLevelVM {
        public string ID { get; set; }
        public string UserID { get; set; }
        public StatusTypesReferencesVM AccessLevel { get; set; }
        public DateTimeStorageVM DateTimeData { get; set; }
        public ApplicationInformationVM Application { get; set; }
        #region static methods
        public static UserAccessLevel set(Guid id, Guid uid, Guid alid, Guid aid, Guid dtid, bool ia) {
            try {
                return new UserAccessLevel() {
                    ID = id,
                    UserID = uid,
                    AccessLevelID = alid,
                    ApplicationID = aid,
                    isArchived = ia,
                    DateTimeStorageID=dtid
                };
            } catch { return null; }
        }
        public static UserAccessLevelVM MToVM(UserAccessLevel model) {
            try {
                return new UserAccessLevelVM() {
                    ID = model.ID.ToString(),
                    UserID = model.UserID.ToString()
                };
            } catch { return null; }
        }
        public static List<UserAccessLevelVM> MsToVMs(List<UserAccessLevel> models) {
            var list = new List<UserAccessLevelVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class UsersVM {
        public string ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
        
        public ImageLinkStorageVM ProfileImage { get; set; }
        public DateTimeStorageVM DateTimeData { get; set; }
        public bool isAllowAccess { get; set; }
        #region static methods
        public static Users set(Guid id, string fname, string lname, string mname, string add, string email, string pass, string cnum, bool isAllow, Guid areg, Guid profID, Guid dtid) {
            try {
                return new Users() {
                    ID=id,
                    Firstname=fname,
                    Lastname=lname,
                    MiddleName=mname,
                    Address=add,
                    EmailAddress=email,
                    ContactNumber=cnum,
                    Password=pass,
                    isAllowAccess=isAllow,
                    ApplicationRegistered=areg,
                    ProfileImageID=profID,
                    DateTimeStorageID=dtid
                };
            } catch { return null; }
        }
        public static UsersVM MToVM(Users model) {
            try {
                return new UsersVM() {
                    ID=model.ID.ToString(),
                    Firstname=model.Firstname,
                    Lastname=model.Lastname,
                    Middlename=model.MiddleName,
                    Address=model.Address,
                    EmailAddress=model.EmailAddress,
                    ContactNumber=model.ContactNumber,
                    isAllowAccess=model.isAllowAccess
                };
            } catch { return null; }
        }
        public static List<UsersVM> MsToVMs(List<Users> models) {
            var list = new List<UsersVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region groupingsData
    public class GroupingsDataVM{
        public string ID { get; set; }
        public string SourceID { get; set; }
        public string OwnerID { get; set; }
        public DateTimeStorageVM DateTimeData { get; set; }
        //model.categoryID
        public StatusTypesReferencesVM CategoryStatus { get; set; }

        #region MessagingPart
        public UsersVM User { get; set; }
        public MessagingRoomVM MessagingRoom { get; set; }
        #endregion

        #region static methods
        public static GroupingsData set(Guid id, Guid sid, Guid oid, Guid aid, int order, Guid dtid, bool ia, Guid catID) {
            try {
                return new GroupingsData() {
                    ID=id,
                    SourceID=sid,
                    OwnerID=oid,
                    API=aid,
                    Order=order,
                    DateTimeStorageID=dtid,
                    isArchived=ia,
                    CategoryID=catID
                };
            } catch { return null; }
        }
        public static GroupingsDataVM MToVM(GroupingsData model) {
            try {
                return new GroupingsDataVM() {
                    ID=model.ID.ToString(),
                    SourceID=model.SourceID.ToString(),
                    OwnerID=model.OwnerID.ToString(),
                };
            } catch { return null; }
        }
        public static List<GroupingsDataVM> MsToVMs(List<GroupingsData> models) {
            var list = new List<GroupingsDataVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class GroupingsInformationVM {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusTypesReferencesVM Category { get; set; }
        #region static methods
        public static GroupingsInformation set(Guid id, string name, string desc, Guid oid, Guid aid, Guid cid) {
            try {
                return new GroupingsInformation() {
                    ID=id,
                    Name=name,
                    Description=desc,
                    OwnerID=oid,
                    API=aid,
                    CategoryID=cid
                };
            } catch { return null; }
        }
        public static GroupingsInformationVM MToVM(GroupingsInformation model) {
            try {
                return new GroupingsInformationVM() {
                    ID=model.ID.ToString(),
                    Name=model.Name,
                    Description=model.Description
                };
            } catch { return null; }
        }
        public static List<GroupingsInformationVM> MsToVMs(List<GroupingsInformation> models) {
            var list = new List<GroupingsInformationVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion

    }

    #endregion
    #region ImageLinkStorage
    public class ImageLinkStorageVM{
        public string ID { get; set; }
        public string Source { get; set; }
        public int Order { get; set; }
        #region static methods
        public static ImageLinkStorage set(Guid id, Guid oid, string source, Guid api, Guid dtid, bool ia, Guid cid, int order) {
            try {
                return new ImageLinkStorage() {
                    ID=id,
                    OwnerID=oid,
                    API=api,
                    Source=source,
                    DateTimeStorageID=dtid,
                    isArchived=ia,
                    CategoryID=cid,
                    Order=order
                };
            } catch { return null; }
        }
        public static ImageLinkStorageVM MToVM(ImageLinkStorage model) {
            try {
                return new ImageLinkStorageVM() {
                    ID=model.ID.ToString(),
                    Source=model.Source,
                    Order=model.Order
                };
            } catch { return null; }
        }
        public static List<ImageLinkStorageVM> MsToVMs(List<ImageLinkStorage> models) {
            var list = new List<ImageLinkStorageVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region StoreManagement
    public class MyStoreVM {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public StatusTypesReferencesVM StoreCategory { get; set; }
        public ImageLinkStorageVM StoreBackgroundImage { get; set; }
        public ImageLinkStorageVM StoreLogo { get; set; }
        public List<LocationStorageVM> BranchLocations { get; set; }
        #region static methods
        public static MyStore set(Guid id, Guid uid, string name, Guid api, Guid scid, Guid sbid, Guid slid, bool ia, Guid dtid) {
            try {
                return new MyStore() {
                    ID=id,
                    UserID=uid,
                    Name=name,
                    ApplicationID=api,
                    StoreCategoryID=scid,
                    StoreBackgroundImageID=sbid,
                    StoreLogoID=slid,
                    isApproved=ia,
                    DateTimeStorageID=dtid
                };
            } catch { return null; }
        }
        public static MyStoreVM MToVM(MyStore model) {
            try {
                return new MyStoreVM()
                {
                    ID = model.ID.ToString(),
                    UserID = model.UserID.ToString(),
                    Name=model.Name,
                };
            } catch { return null; }
        }
        public static List<MyStoreVM> MsToVMs(List<MyStore> models) {
            var list = new List<MyStoreVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class StoreBranchVM
    {
        public string ID { get; set; }
        public string StoreID { get; set; }
        public string AttendantID { get; set; }
        public LocationStorageVM Geolocation { get; set; }
        public string Address { get; set; }
        #region static methods
        public static StoreBranch set(Guid id, Guid sid, Guid aid, Guid gid, string address, Guid dtid) {
            try {
                return new StoreBranch() {
                    ID=id,
                    StoreID=sid,
                    AttendantID=aid,
                    GeoLocationID=gid,
                    Address=address,
                    DateTimeStorageID=dtid,
                };
            } catch { return null; }
        }
        public static StoreBranchVM MToVM(StoreBranch model) {
            try {
                return new StoreBranchVM() {
                    ID=model.ID.ToString(),
                    StoreID=model.StoreID.ToString(),
                    AttendantID=model.AttendantID.ToString(),
                    Address=model.Address
                };
            } catch { return null; }
        }
        public static List<StoreBranchVM> MsToVMs(List<StoreBranch> models) {
            var list = new List<StoreBranchVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }

    #endregion
    #region ReportSystem
    public class ReportClaimsVM {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string SenderUserID { get; set; }
        //report types:
        //Spam, Disturbing, Sexual Harassment, Hacker
        public StatusTypesReferencesVM ReportType { get; set; }
        public string Reason { get; set; }
        public DateTimeStorageVM DateTime { get; set; }

        #region static methods
        public static ReportClaims set(Guid id, Guid uid, Guid suid, Guid rtid, string reason, Guid aid, Guid dtid) {
            try {
                return new ReportClaims() {
                    ID=id,
                    UserID=uid,
                    SenderUserID=suid,
                    ReportTypeID=rtid,
                    Reason=reason,
                    ApplicationID=aid,
                    DateTimeStorageID=dtid
                };
            } catch { return null; }
        }
        public static ReportClaimsVM MToVM(ReportClaims model) {
            try {
                return new ReportClaimsVM() {
                    ID=model.ID.ToString(),
                    UserID=model.UserID.ToString(),
                    SenderUserID=model.SenderUserID.ToString(),
                    Reason=model.Reason,
                };
            } catch { return null; }
        }
        public static List<ReportClaimsVM> MsToVMs(List<ReportClaims> models) {
            var list = new List<ReportClaimsVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region SystemLogs
    public class SystemLogsVM {
        public string ID { get; set; }
        public string Description { get; set; }
        public string OwnerID { get; set; }
        #region static methods
        public static SystemLogs set(Guid id, string desc, Guid oid, Guid aid, Guid dtid, bool ia) {
            try {
                return new SystemLogs() {
                    ID=id,
                    Description=desc,
                    OwnerID=oid,
                    API=aid,
                    DateTimeStorageID=dtid,
                    isArchived=ia
                };
            } catch { return null; }
        }
        public static SystemLogsVM MToVM(SystemLogs model) {
            try {
                return new SystemLogsVM() {
                    ID=model.ID.ToString(),
                    Description=model.Description,
                    OwnerID=model.OwnerID.ToString(),
                };
            } catch { return null; }
        }
        public static List<SystemLogsVM> MsToVMs(List<SystemLogs> models) {
            var list = new List<SystemLogsVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region MessagingRoom
    public class MessagingRoomVM {
        public string ID { get; set; }
        public string Name { get; set; }
        public string OwnerID { get; set; }
        public string API { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        #region static methods
        public static MessagingRoom set(Guid id, string name, Guid oid, Guid aid, Guid dtid, bool ia) {
            try {
                return new MessagingRoom() {
                    ID=id,
                    Name=name,
                    OwnerID=oid,
                    API=aid,
                    DateTimeStorageID=dtid,
                    isArchived=ia
                };
            } catch { return null; }
        }
        public static MessagingRoomVM MToVM(MessagingRoom model) {
            try {
                return new MessagingRoomVM() {
                    ID=model.ID.ToString(),
                    Name=model.Name,
                    OwnerID=model.OwnerID.ToString(),
                    API=model.API.ToString(),
                };
            } catch { return null; }
        }
        public static List<MessagingRoomVM> MsToVMs(List<MessagingRoom> models) {
            var list = new List<MessagingRoomVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class MessagingConversationVM {
        public string ID { get; set; }
        public string Text { get; set; }
        public StatusTypesReferencesVM MessageType { get; set; }
        public string SenderID { get; set; }
        public string RoomID { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        #region static methods
        public static MessagingConversation set(Guid id, string text, Guid mtid, Guid sid, Guid rid, Guid dtid, bool ia) {
            try {
                return new MessagingConversation() {
                    ID=id,
                    Text=text,
                    MessageTypeID=mtid,
                    SenderID=sid,
                    RoomID=rid,
                    DateTimeStorageID=dtid,
                    isArchived=ia
                };
            } catch { return null; }
        }
        public static MessagingConversationVM MToVM(MessagingConversation model) {
            try {
                return new MessagingConversationVM() {
                    ID=model.ID.ToString(),
                    Text=model.Text,
                    SenderID=model.SenderID.ToString(),
                    RoomID=model.RoomID.ToString(),
                };
            } catch { return null; }
        }
        public static List<MessagingConversationVM> MsToVMs(List<MessagingConversation> models) {
            var list = new List<MessagingConversationVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class MessagingConversationReceipentVM {
        public string ID { get; set; }
        public MessagingConversationVM MessagingConversation { get; set; }

        #region static methods
        public static MessagingConversationReceipent set(Guid id, Guid rid, Guid mcid, bool ir, Guid recID) {
            try {
                return new MessagingConversationReceipent() {
                    ID=id,
                    RoomID=rid,
                    MessagingConversationID=mcid,
                    isRead=ir,
                    ReceiverID=recID
                };
            } catch { return null; }
        }
        public static MessagingConversationReceipentVM MToVM(MessagingConversationReceipent model) {
            try {
                return new MessagingConversationReceipentVM() {
                    ID=model.ID.ToString(),
                };
            } catch { return null; }
        }
        public static List<MessagingConversationReceipentVM> MsToVMs(List<MessagingConversationReceipent> models) {
            var list = new List<MessagingConversationReceipentVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }

        #endregion
    }
    #endregion
    #region NotificationManager
    public class NotificationManagerVM {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string OwnerID { get; set; }
        public string API { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        #region static methods
        public static NotificationManager set(Guid id, string title, string message, Guid oid, Guid aid, Guid dtID, bool ir, bool ia) {
            try {
                return new NotificationManager() {
                    ID=id,
                    Title=title,
                    Message=message,
                    OwnerID=oid,
                    API=aid,
                    isRead=ir,
                    DateTimeStorageID=dtID,
                    isArchived=ia,

                };
            } catch { return null; }
        }
        public static NotificationManagerVM MToVM(NotificationManager model) {
            try {
                return new NotificationManagerVM() {
                    ID=model.ID.ToString(),
                    Title=model.Title,
                    Message=model.Message,
                    OwnerID=model.OwnerID.ToString(),
                    API=model.API.ToString(),
                };
            } catch { return null; }
        }
        public static List<NotificationManagerVM> MsToVMs(List<NotificationManager> models) {
            var list = new List<NotificationManagerVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region LocationStorage
    public class LocationStorageVM{
        public string ID { get; set; }
        public StatusTypesReferencesVM LocationCategory { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Description { get; set; }
        #region static methods
        public static LocationStorage set(Guid id, Guid oid, Guid lcid, float longi, float lat, string desc, Guid dtID, bool ia) {
            try {
                return new LocationStorage() {
                    ID=id,
                    OwnerID=oid,
                    LocationCategoryID=lcid,
                    Longitude=longi,
                    Latitude=lat,
                    Description=desc,
                    DateTimeStorageID=dtID,
                    isArchived=ia
                };
            } catch { return null; }
        }
        public static LocationStorageVM MToVM(LocationStorage model) {
            try {
                return new LocationStorageVM() {
                    ID=model.ID.ToString(),
                    Longitude=model.Longitude,
                    Latitude=model.Latitude,
                    Description=model.Description
                };
            } catch { return null; }
        }
        public static List<LocationStorageVM> MsToVMs(List<LocationStorage> models) {
            var list = new List<LocationStorageVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region ReviewStorage
    public class ReviewStoragesVM {
        public string ID { get; set; }
        public string SenderID { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Stars { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        #region static methods
        public static ReviewStorages set(Guid id, Guid sid, Guid rid, Guid aid, string title, string comment, int stars, Guid dtsID, bool ia) {
            try {
                return new ReviewStorages(){
                    ID=id,
                    SenderID=sid,
                    ReviewedID=rid,
                    API=aid,
                    Title=title,
                    Comment=comment,
                    Stars=stars,
                    DateTimeStorageID=dtsID,
                    isArchived=ia
                };
            } catch { return null; }
        }
        public static ReviewStoragesVM MToVM(ReviewStorages model) {
            try {
                return new ReviewStoragesVM() {
                    ID=model.ID.ToString(),
                    SenderID=model.SenderID.ToString(),
                    Title=model.Title,
                    Comment=model.Comment,
                    Stars=model.Stars
                };
            } catch { return null; }
        }
        public static List<ReviewStoragesVM> MsToVMs(List<ReviewStorages> models) {
            var list = new List<ReviewStoragesVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region InventorySystem
    public class IS_ItemVM {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string OwnerID { get; set; }
        public bool isCount { get; set; }
        public int Quantity { get; set; }
        public StatusTypesReferencesVM ItemCategory { get; set; }
        public DateTimeStorageVM DateTimeStorage { get; set; }
        public List<IS_ItemColorVM> ItemColors { get; set; }
        public List<ImageLinkStorageVM> Images { get; set; }
        public StatusTypesReferencesVM Condition { get; set; }
        #region static methods
        public static IS_Item set(Guid id, string title, string desc, float price, Guid aid, Guid oid, Guid icid, bool ic, int q, Guid dtid, Guid cid) {
            try {
                return new IS_Item() {
                    ID=id,
                    Title=title,
                    Description=desc,
                    Price=price,
                    OwnerID=oid,
                    ItemCategoryID=icid,
                    isCount=ic,
                    Quantity=q,
                    DateTimeStorageID=dtid,
                    ConditionID=cid
                };
            } catch { return null; }
        }
        public static IS_ItemVM MToVM(IS_Item model) {
            try {
                return new IS_ItemVM() {
                    ID = model.ID.ToString(),
                    Title = model.Title,
                    Description=model.Description,
                    Price=model.Price,
                    OwnerID=model.OwnerID.ToString(),
                    isCount=model.isCount,
                    Quantity=model.Quantity,
                };
            } catch { return null; }
        }
        public static List<IS_ItemVM> MsToVMs(List<IS_Item> models) {
            var list = new List<IS_ItemVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class IS_ItemStockVM{
        public string ID { get; set; }
        public string BarcodeNumber { get; set; }
        public string SKU { get; set; }
        public string SerialNumber { get; set; }
        public StatusTypesReferencesVM StockStatus { get; set; }
        public DateTimeStorageVM DateTimeStorage { get; set; }
        public IS_ItemColor ItemColor { get; set; }
        public IS_Item Item { get; set; }
        #region static method
        public static IS_ItemStock set(Guid id, string bcode, string sku, string serNum, Guid iid, Guid ssid, Guid dtid, Guid icid) {
            try {
                return new IS_ItemStock() {
                    ID=id,
                    BarcodeNumber=bcode,
                    SKU=sku,
                    SerialNumber=serNum,
                    ItemID=iid,
                    StockStatusID=ssid,
                    DateTimeStorageID=dtid,
                    ItemColorID=icid
                };
            } catch { return null; }
        }
        public static IS_ItemStockVM MToVM(IS_ItemStock model) {
            try {
               return new IS_ItemStockVM() {
                ID=model.ID.ToString(),
                BarcodeNumber=model.BarcodeNumber,
                SKU=model.SKU,
                SerialNumber=model.SerialNumber
               };
            } catch { return null; }
        }
        public static List<IS_ItemStockVM> MsToVMs(List<IS_ItemStock> models) {
            var list = new List<IS_ItemStockVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class IS_ItemColorVM{
        public string ID { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public StatusTypesReferencesVM Category { get; set; }
        public List<ImageLinkStorageVM> Images { get; set; }
        #region static methods
        public static IS_ItemColor set(Guid id, string color, string desc, Guid oid, Guid aid, Guid cid) {
            try {
                return new IS_ItemColor() {
                    ID=id,
                    Color=color,
                    Description=desc,
                    OwnerID=oid,
                    API=aid,
                    CategoryID=cid,
                };
            } catch { return null; }
        }
        public static IS_ItemColorVM MToVM(IS_ItemColor model) {
            try {
                return new IS_ItemColorVM(){
                    ID=model.ID.ToString(),
                    Color=model.Color,
                    Description=model.Description
                };
            } catch{ return null; }
        }
        public static List<IS_ItemColorVM> MsToVMs(List<IS_ItemColor> models) {
            var list = new List<IS_ItemColorVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion

    }

    #endregion
    #region DateTimeStorage
    public class DateTimeStorageVM {
        public string ID { get; set; }
        public string CreatedAtString { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedAtString { get; set; }
        public DateTime UpdatedAt { get; set; }
        #region static method
        public static DateTimeStorage set(Guid id, Guid oid, Guid aid, DateTime ca, DateTime ua, Guid cid) {
            try {
                return new DateTimeStorage() {
                    ID=id,
                    OwnerID=oid,
                    API=aid,
                    UpdatedAt=ua,
                    CreatedAt=ca,
                    CategoryID=cid
                };
            } catch { return null; }
        }
        public static DateTimeStorageVM MToVM(DateTimeStorage model) {
            try {
                return new DateTimeStorageVM(){
                    ID=model.ID.ToString(),
                    CreatedAt=model.CreatedAt,
                    UpdatedAt=model.UpdatedAt,
                    CreatedAtString=DateTimeUtil.DateTimeToString(model.CreatedAt),
                    UpdatedAtString=DateTimeUtil.DateTimeToString(model.UpdatedAt)
                };
            } catch { return null; }
        }
        public static List<DateTimeStorageVM> MsToVMs(List<DateTimeStorage> models) {
            var list = new List<DateTimeStorageVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region QuizMaker
    public class QuizInfoVM {
        public string ID { get; set; }
        public string Name { get; set; }
        public string QuizCode { get; set; }
        public bool hasTimeLimit { get; set; } 
        //Open or Closed
        public StatusTypesReferencesVM QuizStatus { get; set; }
        //Survey or Questionaire
        public StatusTypesReferencesVM Status { get; set; }
        public DateTimeStorageVM DateTimeStorage { get; set; }
        #region static methods
        public static QuizInfo set(Guid id, string name, Guid oid, Guid aid, string qc, bool htl, Guid sid, Guid qsid, Guid dtid) {
            try {
                return new QuizInfo() {
                    ID=id,
                    Name=name,
                    OwnerID=oid,
                    ApplicationID=aid,
                    QuizCode=qc,
                    hasTimeLimit=htl,
                    Status=sid,
                    QuizStatus=qsid,
                    DateTimeStorageID=dtid
                };
            } catch { return null; }
        }
        public static QuizInfoVM MToVM(QuizInfo model) {
            try {
                return new QuizInfoVM() {
                    ID=model.ID.ToString(),
                    Name=model.Name,
                    QuizCode=model.QuizCode,
                    hasTimeLimit=model.hasTimeLimit,
                };
            } catch { return null; }
        }
        public static List<QuizInfoVM> MsToVMs(List<QuizInfo> models) {
            var list = new List<QuizInfoVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class QuizQuestionsVM {
        public string ID { get; set; }
        public string Questions { get; set; }
        public int Order { get; set; }
        public int Points { get; set; }
        public StatusTypesReferencesVM Status { get; set; }
        public List<ImageLinkStorageVM> Images { get; set; }
        public List<QuizQuestionAnswerVM> Choices { get; set; }
        public List<QuizUserAnswerVM> UserAnswers { get; set; }
        #region static methods
        public static QuizQuestions set(Guid id, string ques, Guid qiid, int order, int points, Guid statID) {
            try {
                return new QuizQuestions() {
                    ID=id,
                    Questions=ques,
                    QuizInfoID=qiid,
                    Order=order,
                    Points=points,
                    Status=statID
                };
            } catch { return null; }
        }
        public static QuizQuestionsVM MToVM(QuizQuestions model) {
            try {
                return new QuizQuestionsVM() {
                    ID=model.ID.ToString(),
                    Questions=model.Questions,
                    Order=model.Order,
                    Points=model.Points,
                };
            } catch { return null; }
        }
        public static List<QuizQuestionsVM> MsToVMs(List<QuizQuestions> models) {
            var list = new List<QuizQuestionsVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class QuizQuestionAnswerVM {
        public string ID { get; set; }
        public string Description { get; set; }
        public float Points { get; set; }
        public bool isCorrect { get; set; }
        public float Percent { get; set; }
        public List<ImageLinkStorageVM> Images { get; set; }
        public bool isSelected { get; set; }
        #region static methods
        public static QuizQuestionAnswer set(Guid id, string desc, float points, bool ic, Guid qqid) {
            try {
                return new QuizQuestionAnswer() {
                    ID=id,
                    Description=desc,
                    Points=points,
                    isCorrect=ic,
                    QuizQuestionsID=qqid
                };
            } catch { return null; }
        }
        public static QuizQuestionAnswerVM MToVM(QuizQuestionAnswer model) {
            try {
                return new QuizQuestionAnswerVM() {
                    ID=model.ID.ToString(),
                    Description=model.Description,
                    Points=model.Points,
                    isCorrect=model.isCorrect
                };
            } catch { return null; }
        }
        public static List<QuizQuestionAnswerVM> MsToVMs(List<QuizQuestionAnswer> models) {
            var list = new List<QuizQuestionAnswerVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #region User Ques Answers
    public class QuizTakersVM{
        public string ID { get; set; }
        public string UserID { get; set; }
        public float TotalPoints { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        public QuizInfoVM QuizInfo { get; set; }
        public UsersVM User { get; set; }
        #region static methods
        public static QuizTakers set(Guid id, Guid qiid, Guid uid, float tp, Guid dtid, Guid aid) {
            try {
                return new QuizTakers() {
                    ID=id,
                    QuizInfoID=qiid,
                    UserID=uid,
                    DateTimeStorageID=dtid,
                    API=aid,
                    TotalPoints=0,
                };
            } catch { return null; }
        }
        public static QuizTakersVM MToVM(QuizTakers model) {
            try {
                return new QuizTakersVM() {
                    ID=model.ID.ToString(),
                    UserID=model.UserID.ToString(),
                    TotalPoints=model.TotalPoints,
                };
            } catch { return null; }
        }
        public static List<QuizTakersVM> MsToVMs(List<QuizTakers> models) {
            var list = new List<QuizTakersVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class QuizUserAnswerVM {
        public string ID { get; set; }
        public string QuizQuestionID { get; set; }
        public string QuizAnswerID { get; set; }
        public string OtherAnswer { get; set; }
        public float PointsEarned { get; set; }
        #region static methods
        public static QuizUserAnswer set(Guid id, Guid qtid, Guid qqid, Guid qaid, string oa, float pe) {
            try {
                return new QuizUserAnswer() {
                    ID=id,
                    QuizTakersID=qtid,
                    QuizQuestionID=qqid,
                    QuizAnswerID=qaid,
                    OtherAnswer=oa,
                    PointsEarned=pe
                };
            } catch { return null; }
        }
        public static QuizUserAnswerVM MToVM(QuizUserAnswer model) {
            try {
                return new QuizUserAnswerVM() {
                    ID=model.ID.ToString(),
                    QuizAnswerID=model.QuizAnswerID.ToString(),
                    QuizQuestionID=model.QuizQuestionID.ToString(),
                    OtherAnswer=model.OtherAnswer,
                    PointsEarned=model.PointsEarned
                };
            } catch { return null; }
        }
        public static List<QuizUserAnswerVM> MsToVMs(List<QuizUserAnswer> models) {
            var list = new List<QuizUserAnswerVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #endregion
    #region data collection
    public class EmailListVM {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        #region static methods
        public static EmailList set(Guid id, string name, string email, Guid dtid, string cnum) {
            try{
                return new EmailList() {
                    ID=id,
                    Name=name,
                    Email=email,
                    DateTimeID=dtid,
                    ContactNumber=cnum
               };
            } catch { return null; }
        }
        public static EmailListVM MToVM(EmailList model) {
            try {
                return new EmailListVM() {
                    ID=model.ID.ToString(),
                    Name=model.Name,
                    Email=model.Email
                };
            } catch { return null; }
        }
        public static List<EmailListVM> MsToVMs(List<EmailList> models) {
            var list = new List<EmailListVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class SearchInputsDataVM {
        public string ID { get; set; }
        public string SearchInput { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        #region static methods
        public static SearchInputsData set(Guid id, string sinput, Guid oid, Guid dtid, Guid api) {
            try {
                return new SearchInputsData() {
                    ID=id,
                    SearchInput=sinput,
                    OwnerID=oid,
                    DateTimeID=dtid,
                    API=api
                };
            } catch { return null; }
        }
        public static SearchInputsDataVM MToVM(SearchInputsData model) {
            try {
                return new SearchInputsDataVM() {
                    ID=model.ID.ToString(),
                    SearchInput=model.SearchInput,
                };
            } catch { return null; }
        }
        public static List<SearchInputsDataVM> MsToVMs(List<SearchInputsData> models){
            var list = new List<SearchInputsDataVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region Customer Order
    public class CustomerOrderVM {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string OwnerID { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        public List<CustomerOrderItemVM> OrderItem { get; set; }
        public bool isSubmit { get; set; }
        public float TotalCost { get; set; }
        #region static methods
        public static CustomerOrder set(Guid id, Guid uid, Guid oid, Guid aid, Guid dtid, bool iS, float tc) {
            try {
                return new CustomerOrder()
                {
                    ID=id,
                    UserID=uid,
                    OwnerID=oid,
                    API=aid,
                    DateTimeID=dtid,
                    isSubmit=iS,
                    TotalCost=tc
                };
            } catch { return null; }
        }
        public static CustomerOrderVM MToVM(CustomerOrder model) {
            try {
                return new CustomerOrderVM() {
                    ID=model.ID.ToString(),
                    UserID=model.UserID.ToString(),
                    OwnerID=model.OwnerID.ToString(),
                    isSubmit=model.isSubmit,
                    TotalCost=model.TotalCost
                };
            } catch { return null; }
        }
        public static List<CustomerOrderVM> MsToVMs(List<CustomerOrder> models) {
            var list = new List<CustomerOrderVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class CustomerOrderItemVM {
        public Guid ID { get; set; }
        public float SubCost { get; set; }
        public int Quantity { get; set; }

        public IS_ItemVM Item { get; set; }
        public IS_ItemColorVM ItemColor { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        #region static methods
        public static CustomerOrderItem set(Guid id, Guid iid, Guid icid, float sc, int q, Guid coid, Guid aid, Guid dtid) {
            try {
                return new CustomerOrderItem() {
                    ID=id,
                    ItemID=iid,
                    ItemColorID=icid,
                    SubCost=sc,
                    Quantity=q,
                    CustomerOrderID=coid,
                    API=aid,
                    DateTimeID=dtid
                };
            } catch { return null; }
        }
        public static CustomerOrderItemVM MToVM(CustomerOrderItem model) {
            try {
                return new CustomerOrderItemVM() {
                    ID=model.ID,
                    SubCost=model.SubCost,
                    Quantity=model.Quantity
                };
            } catch { return null; }
        }
        public static List<CustomerOrderItemVM> MsToVMs(List<CustomerOrderItem> models) {
            var list = new List<CustomerOrderItemVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion

    }
    #endregion
    #region ShopManagement
    public class ShopManagementVM {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageLinkStorageVM ProfileImage { get; set; }
        public List<IS_ItemVM> Items { get; set; }
        #region static methods
        public static ShopManagement set(Guid id, string name, string desc, Guid oid, Guid aid, Guid pi, Guid cid) {
            try {
                return new ShopManagement() {
                    ID=id,
                    Name=name,
                    Description=desc,
                    OwnerID=oid,
                    API=aid,
                    ProfileImage=pi,
                    CategoryID=cid
                };
            } catch { return null; }
        }
        public static ShopManagementVM MToVM(ShopManagement model){
            try{
                return new ShopManagementVM() {
                    ID=model.ID.ToString(),
                    Name=model.Name,
                    Description=model.Description,
               };
            } catch { return null; }
        }
        public static List<ShopManagementVM> MsToVMs(List<ShopManagement> models) {
            var list = new List<ShopManagementVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region DescriptionStorage
    public class DescriptionStorageVM{
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public StatusTypesReferencesVM Category { get; set; }

        #region static methods
        public static DescriptionStorage set(Guid id, string title, string desc, Guid oid, Guid aid, Guid cid, int order) {
            try {
                return new DescriptionStorage() {
                    ID=id,
                    Title=title,
                    Description=desc,
                    OwnerID=oid,
                    API=aid,
                    CategoryID=cid,
                    Order=order
                };
            } catch { return null; }
        }
        public static DescriptionStorageVM MToVM(DescriptionStorage model) {
            try {
                return new DescriptionStorageVM() {
                    ID=model.ID.ToString(),
                    Title=model.Title,
                    Description=model.Description,
                    Order=model.Order,
                };
            } catch { return null; }
        }
        public static List<DescriptionStorageVM> MsToVMs(List<DescriptionStorage> models) {
            var list = new List<DescriptionStorageVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }

    #endregion
    #region InvitationStorage

    #endregion
    #region DocumentAttachment
    //image or link
    #endregion
    #region ContentManagement
    public class LeadPagesVM {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerID { get; set; }
        public ImageLinkStorageVM BackgroundImage { get; set; }
        public ImageLinkStorageVM MainImage { get; set; }
        public StatusTypesReferencesVM TemplateDesign { get; set; }

        #region static methods
        public static LeadPages set(Guid id, string title, string desc, Guid oid, Guid bgid, Guid mid, Guid tdid) {
            try {
                return new LeadPages() {
                    ID=id,
                    Title=title,
                    Description=desc,
                    OwnerID=oid,
                    BackgroundImageID=bgid,
                    MainImageID=mid,
                    TemplateDesignID=tdid
                };
            } catch { return null; }
        }
        public static LeadPagesVM MToVM(LeadPages model) {
            try {
                return new LeadPagesVM() {
                    ID=model.ID.ToString(),
                    Title=model.Title,
                    Description=model.Description,
                    OwnerID=model.OwnerID.ToString(),
                };
            } catch { return null; }
        }
        public static List<LeadPagesVM> MsToVMs(List<LeadPages> models) {
            var list = new List<LeadPagesVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region URLStorage
    public class URLStorageVM {
        public string ID { get; set; }
        public string Link { get; set; }

    }
    #endregion
    #region securityStuff
    public class SecurityLinksVM {
        public string URL { get; set; }
        public string OwnerID { get; set; }
        #region static methods
        public static SecurityLinks set(Guid id, Guid cid, string url, Guid oid) {
            try {
                return new SecurityLinks() {
                    ID=id,
                    CategoryID=cid,
                    URL=url,
                    OwnerID=oid
                };
            } catch { return  null; }
        }
        public static SecurityLinksVM MToVM(SecurityLinks model) {
            try {
                return new SecurityLinksVM() {
                    OwnerID=model.OwnerID.ToString(),
                    URL=model.URL
                };
            } catch { return null; }
        }
        public static List<SecurityLinksVM> MsToVMs(List<SecurityLinks> models) {
            var list = new List<SecurityLinksVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region ScoreStorage
    public class ScoreStorageVM
    {
        public string ID { get; set; }
        public string API { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public DateTimeStorageVM DateTime { get; set; }
        #region static methods
        public static ScoreStorage set(Guid id, Guid aid, string name, int score, Guid dtid) {
            try {
                return new ScoreStorage(){
                    ID=id,
                    API=aid,
                    Name=name,
                    Score=score,
                    DateTimeID=dtid
                };
            } catch { return null; }
        }
        public static ScoreStorageVM MToVM(ScoreStorage model) {
            try {
                return new ScoreStorageVM() {
                    ID = model.ID.ToString(),
                    API = model.API.ToString(),
                    Name=model.Name,
                    Score=model.Score
                };
            } catch { return null; }
        }

        #endregion
    }



    #endregion


}
