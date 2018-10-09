using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Models.CPS
{
    #region MyCompanyInformation
    //everything related to the company data is stored here
    public class ApplicationInformation{
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
    }
    //stores information about the ads that email address used to login in funnels and stuff
    public class AdsInformation {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string AdInformation { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string URL { get; set; }
        //ADImage, ADText
        public Guid Category { get; set; }
        //Electronics, Health and LifeStyle
        public Guid DateTimeID { get; set; }
    }
    /*
        GroupingsData- contains the AdCategory of the AdsInformation 
        SourceID=StatusTypeReference (Electronics, Health and Lifestyle, Shoes, Clothings)
        OwnerID=AdInformationID
        CategoryID=Ads
    */

    #endregion
    #region status references
    public class StatusTypesReferences
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        //owner of the data
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        public Guid DateTimeStorageID { get; set; }
        //this will determine if this status type is 
        //ex. ItemCategory, UserAccessLevel, StoreTypes and etc
        //category list
        // ItemCategory, UserAccessLevel, QuizTestTypes, QuizQuestionType, SubStatus
        public Guid CategoryID { get; set; }

    }
    #endregion
    #region user Related Information
    //this is where i store the data of the signalR id of users 
    public class SignalRData {
        public Guid ID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid SignalRID { get; set; }
        public Guid API { get; set; }
        public Guid DateTimeID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Hub { get; set; }
        public bool isActive { get; set; }
    }
    //this is where i store data about the user access level information of each app
    public class UserAccessLevel
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        //this will get reference from status type references
        public Guid AccessLevelID { get; set; }
        //determines where this accesslevel is valid for use
        public Guid ApplicationID { get; set; }
        public Guid DateTimeStorageID { get; set; }
        public bool isArchived { get; set; }
    }
    public class Users
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public String Firstname { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public String Lastname { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public String MiddleName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public String Address { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public String EmailAddress { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public String ContactNumber { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Password { get; set; }
        //determines if this account can access the system
        public bool isAllowAccess { get; set; }
        //this tells which app the user registered his account
        public Guid ApplicationRegistered { get; set; }
        public Guid ProfileImageID { get; set; }
        public Guid DateTimeStorageID { get; set; }
    }

    #endregion
    #region Data Groupings
    //group data
    //purpose of this table is to group related data it can be used by anything
    //ex. group of images, group of items etc....
    public class GroupingsData
    {
        public Guid ID { get; set; }
        public Guid SourceID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        //order being displayed
        public int Order { get; set; }
        public Guid DateTimeStorageID { get; set; }
        //this will determine which type of data is being kept here
        /*
             MessagingRoomParticipant, ItemImages, BuyAndSellCategory, BuyAndSellSubCategory, MyStoreGPSLocation, UserInterest
        */
        public Guid CategoryID { get; set; }
        public bool isArchived { get; set; }
    }
    public class GroupingsInformation {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        public Guid CategoryID { get; set; }
    }


    #endregion
    #region Image Storage
    //purpose of this table is to storage of ImageLinkStorage
    //which can be used by any which uses the imageStorage functionalities
    public class ImageLinkStorage
    {
        public Guid ID { get; set; }
        public Guid API { get; set; }
        //can be userID, QuestionID or etc
        public Guid OwnerID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Source { get; set; }
        public Guid DateTimeStorageID { get; set; }
        public bool isArchived { get; set; }
        //quizQuestion, QuizQuestionAnswer,  
        public Guid CategoryID { get; set; }
        public int Order { get; set; }
    }
    #endregion
    #region StoreManagement
    public class MyStore
    {
        public Guid ID { get; set; }
        //owner of the store
        //represent ownerID
        public Guid UserID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        //determines where this store will appear in my application wether its in the geomarket, delivery system and etc
        public Guid ApplicationID { get; set; }
        //determine the category of the store if its a general, food, clothing and etc
        public Guid StoreCategoryID { get; set; }
        //this will determine the address of the image where its stored in ImageLinkStorage in uploaders
        public Guid StoreBackgroundImageID { get; set; }
        public Guid StoreLogoID { get; set; }
        public bool isApproved { get; set; }
        public Guid DateTimeStorageID { get; set; }
    }
    public class StoreBranch
    {
        public Guid ID { get; set; }
        public Guid StoreID { get; set; }
        public Guid AttendantID { get; set; }
        public Guid GeoLocationID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Address { get; set; }
        public Guid DateTimeStorageID { get; set; }
    }
    #endregion
    #region ReportSystem
    public class ReportClaims
    {
        public Guid ID { get; set; }
        //user being reported id being reported
        public Guid UserID { get; set; }
        //creator of the report
        public Guid SenderUserID { get; set; }
        //can be stores, users, etc id is stored in status reference tables
        public Guid ReportTypeID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Reason { get; set; }
        public Guid ApplicationID { get; set; }
        public Guid DateTimeStorageID { get; set; }
    }
    #endregion
    #region System Logs
    public class SystemLogs
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        public Guid DateTimeStorageID { get; set; }
        public bool isArchived { get; set; }
    }
    #endregion
    #region Messaging Services
    //instant messaging app is here
    public class MessagingRoom
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        //the one who created the chat room
        public Guid OwnerID { get; set; }
        //this will determine which application this room belongs to
        public Guid API { get; set; }
        public Guid DateTimeStorageID { get; set; }
        public bool isArchived { get; set; }
    }
    //Participant of the room is in groupings data
    //SourceID=UserID participant
    //OwnerID=MessagingRoomID
    
    // contains the messages and stuff for the room
    public class MessagingConversation
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string Text { get; set; }
        //data is stored in status type reference
        //text, image, etc
        public Guid MessageTypeID { get; set; }
        public Guid SenderID { get; set; }
        public Guid RoomID { get; set; }
        public Guid DateTimeStorageID { get; set; }
        public bool isArchived { get; set; }
    }
    public class MessagingConversationReceipent{
        public Guid ID { get; set; }
        public Guid RoomID { get; set; }
        public Guid MessagingConversationID { get; set; }
        public bool isRead { get; set; }
        public Guid ReceiverID { get; set; }
        
    }

    #endregion
    #region Notification Services
    //all notification related stuff are stored in here
    public class NotificationManager
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Message { get; set; }
        //owner of the Notification
        public Guid OwnerID { get; set; }
        //uid where you store the owner of this notification
        public Guid API { get; set; }
        public Guid DateTimeStorageID { get; set; }
        //this will determine if notification is read if not then send it to the receiver
        public bool isRead { get; set; }
        public bool isArchived { get; set; }
    }
    //Receipent is stored in groupings data
    //Source = SenderID
    //OwnerID = NotificationManagerID
    //How to track if there is a notification for this user
    //groupData category NotificationManagerData

    #endregion
    #region LocationStorage
    //basically it stores the location based data here
    //either user location, item location etc
    public class LocationStorage
    {
        public Guid ID { get; set; }
        //contains the owner of the locationStorage
        public Guid OwnerID { get; set; }
        //used to identify which type of location is this
        //UserLocation, 
        //referece is stored in status type reference
        public Guid LocationCategoryID { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        //store anything in here
        //either nearest city etc data, ex. Davao City, General Santos City
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public Guid DateTimeStorageID { get; set; }
        public bool isArchived { get; set; }
    }

    #endregion
    #region ReviewStorages
    //all reviews made are stored here
    public class ReviewStorages {
        public Guid ID { get; set; }
        //creator of the review
        public Guid SenderID { get; set; }
        //id being reviewed
        public Guid ReviewedID { get; set; }
        //this will differentiate the data so that if there is same reviewed they
        //will vary based on where the review was made
        public Guid API { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Comment { get; set; }
        public int Stars { get; set; }
        public Guid DateTimeStorageID { get; set; }
        public bool isArchived { get; set; }
    }
    #endregion
    #region InventorySystem
    public class IS_Item{
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        //this api refers to application api
        public Guid API { get; set; }
        public float Price { get; set; }
        //this one determines the owner of the item
        //it can be stores or users etc
        public Guid OwnerID { get; set; }
        //item category stored in status type reference
        // Accessories, General, Books and Etc 
        public Guid ItemCategoryID { get; set; }
        //2 types of items with serial or just countable
        //if its just count then it does not need particular identifier to each item ex. eggs, foods etc
        //serial are items with specific identity means each item can be identified using its serial or barcode number
        public bool isCount { get; set; }
        public int Quantity { get; set; }
        //data in date time storage
        public Guid DateTimeStorageID { get; set; }
        //Brandnew or Second hand - StatusTypeReference
        public Guid ConditionID { get; set; }
    }
    //item images is in group data
    public class IS_ItemStock {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string BarcodeNumber { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string SerialNumber { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string SKU { get; set; }
        //this will determine which item this belongs to
        public Guid ItemID { get; set; }
        //data is in status type reference
        //stock out, available, and etc
        public Guid StockStatusID { get; set; }
        //date stocked in and date updated
        public Guid DateTimeStorageID { get; set; }
        public Guid ItemColorID { get; set; }
    }
    //contains the addon stuff for the product and its color
    public class IS_ItemColor {
        public Guid ID { get; set; }
        public string Color { get; set; }
        //description
        public string Description { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        //determine which data of table this belongs to
        public Guid CategoryID { get; set; }
    }
    public class IS_ItemPromo {
        public Guid ID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        public Guid CategoryID { get; set; }
        //price/percentOff*100=
        public float PercentOff { get; set; }
        //price-priceoff= new price
        public float PriceOff { get; set; }
        //determine the validity of the sale promo
        public Guid StartDateID { get; set; }
        public Guid EndDateID { get; set; }
    }


    #endregion
    #region DateTimeStorage
    public class DateTimeStorage{
        public Guid ID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //category in status reference
        //ex. Begin and End, Created and Updated
        public Guid CategoryID { get; set; }
    }
    #endregion
    #region QuizMaker
    public class QuizInfo
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        //states owner of this Question
        public Guid OwnerID { get; set; }
        public Guid ApplicationID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string QuizCode { get; set; }
        public bool hasTimeLimit { get; set; }
        //Questionaire, Survey
        public Guid Status { get; set; }
        //open, closed
        public Guid QuizStatus { get; set; }
        public Guid DateTimeStorageID { get; set; }
    }
    public class QuizQuestions
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Questions { get; set; }
        public Guid QuizInfoID { get; set; }
        public int Order { get; set; }
        //points possible to earn in this question
        public int Points { get; set; }
        //identifies if this question is wether a Essay or multiple choice
        public Guid Status { get; set; }
    }
    //quizQuestions images is in imageLinkStorage
    //ownerID=quizQuestionID

    public class QuizQuestionAnswer
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        //points earned for selecting this choice
        public float Points { get; set; }
        public bool isCorrect { get; set; }
        public Guid QuizQuestionsID { get; set; }
    }
    //quizQuestionAnswer images is in groupingsData
    #region User Ques Answers
    public class QuizTakers
    {
        public Guid ID { get; set; }
        public Guid QuizInfoID { get; set; }
        public Guid UserID { get; set; }
        //total points earned by this quizTaker
        public float TotalPoints { get; set; }
        public Guid DateTimeStorageID { get; set; }
        public Guid API { get; set; }
    }
    public class QuizUserAnswer {
        public Guid ID { get; set; }
        public Guid QuizTakersID { get; set; }
        public Guid QuizQuestionID { get; set; }
        public Guid QuizAnswerID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string OtherAnswer { get; set; }
        public float PointsEarned { get; set; }
    }

    #endregion
    #endregion
    #region data collection
    public class EmailList{
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public Guid DateTimeID { get; set; }
    }
    /*
        Groupings data contains the particular interest of EmailList 
        SourceID=StatusTypeReference(Electronics, Health and Lifestyle, Shoes, Clothings, Anime)
        OwnerID=EmailListID
    */

    //collects/saves information of the user input based on its search
    public class SearchInputsData{
        public Guid ID { get; set; }
        public string SearchInput { get; set; }
        public Guid OwnerID { get; set; }
        public Guid DateTimeID { get; set; }
        public Guid API { get; set; }
    }


    #endregion
    #region ContentManagement
    public class ContentManagementSettings {
        public Guid ID { get; set; }
        //owner of this content settings
        public Guid OwnerID { get; set; }
        //css class must be stored here
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string PrimaryColor { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string SecondaryColor { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Width { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Height { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        //align-center etc
        public string PositionAlign { get; set; }
        public Guid API { get; set; }
        public Guid DateTimeID { get; set; }
    }

    #endregion
    #region CustomerOrder
    /* ReadMe
        CustomerOrder
            -OwnerID=Shop, UserID where it was ordered  
    */
    public class CustomerOrder{
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        public Guid DateTimeID { get; set; }
        public bool isSubmit { get; set; }
        public float TotalCost { get; set; }
    }
    public class CustomerOrderItem{
        public Guid ID { get; set; }
        public Guid ItemID { get; set; }
        //this will identify what time of item will i pull out in my inventory
        //sku identify if this item is blue, black or red based on its sku wether its size or 2GB 2TB or etc
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string ItemSKU { get; set; }
        public Guid ItemColorID { get; set; }
        public float SubCost { get; set; }
        public int Quantity { get; set; }
        public Guid CustomerOrderID { get; set; }
        public Guid API { get; set; }
        public Guid DateTimeID { get; set; }
    }
    #endregion
    #region ShopManagement
    public class ShopManagement {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        public Guid ProfileImage { get; set; }
        //this determines what kind of shop this is
        //clothing, general, food and etc
        public Guid CategoryID { get; set; }

    }
    #endregion
    #region DescriptionStorage
    /*Readme
        so data can look like this

        HelloWorld-Test description
        Hello World
        -Test Description
        CategoryID
        *IS_Item, CustomerOrder and etc

    */

    public class DescriptionStorage
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }
        public Guid CategoryID { get; set; }
        public int Order { get; set; }
    }
    #endregion
    #region InvitationStorage
    public class InvitationStorage {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public Guid API { get; set; }
        /*
         groupInvitation, groupChatInvitation, and etc
         */
        public Guid CategoryID { get; set; }
        /*
         *this is where you store the dataID if you invited 
         * ex. Join in Group store groupID here
         */
        public Guid StorageID { get; set; }
        //invited by UserID and etc
        public Guid OwnerID { get; set; }
    }
    #endregion
    #region DocumentAttachmentStorage
    /*
        Store valuable information about the users or stores etc 
    */
    public class DocumentAttachmentStorage {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public Guid ImageAttachmentID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string URL { get; set; }
        public Guid OwnerID { get; set; }
        public Guid API { get; set; }

    }
    #endregion
    #region Content Management

    /*Customer Funnel
     
    */
    public class LeadPages{
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Description { get; set; }
        public Guid OwnerID { get; set; }
        public Guid BackgroundImageID { get; set; }
        public Guid MainImageID { get; set; }
        public Guid TemplateDesignID { get; set; }
    }
    #endregion
    #region URLStorage
    public class URLStorage {
        public Guid ID { get; set; }
        public Guid OwnerID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength] 
        public string Link { get; set; }
        public Guid API { get; set; }

    }
    #endregion
    #region SecurityStuff
    public class SecurityLinks {
        public Guid ID { get; set; }
        /*
            UserPasswordChange-enable user to change password  
        */
        public Guid CategoryID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        /*
         * this link will be sent  to the users email in to proceed to page
            http://url.com/IDwithoutDashes
         */
        public string URL { get; set; }
        //password to update
        public Guid OwnerID { get; set; }

    }
    #endregion
    #region ScoreStorage
    public class ScoreStorage {
        public Guid ID { get; set; }
        public Guid API { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public Guid DateTimeID { get; set; }
    }

    #endregion


}
