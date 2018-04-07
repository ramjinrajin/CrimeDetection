 

CREATE TABLE [dbo].[tblCrime](
	[CrimeId] [int] IDENTITY(1,1) NOT NULL,
	[Crime] [varchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[Criminal] [varchar](100) NULL,
	[Location] [varchar](100) NULL,
	[IsWomensCrime] [int] NOT NULL,
	[UserId] [int] NULL,
	[Status] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CrimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblCrime] ADD  DEFAULT ((0)) FOR [IsWomensCrime]
GO
 

CREATE TABLE [dbo].[CrimeUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[EmailId] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[ContactNo] [varchar](100) NULL,
	[District] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

 

CREATE TABLE [dbo].[CrimeComments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[CrimeId] [int] NULL,
	[CommentDesc] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

 

 CREATE  PROC SpUpdateCrimeStatusAndComments
 (
  @CrimeId int,
  @Status varchar(100),
  @Comment  nvarchar(max)
 )
 AS
 BEGIN
   Update tblCrime SET Status=@Status where CrimeId=@CrimeId
   INSERT into CrimeComments (CrimeId,CommentDesc) VALUES (@CrimeId,@Comment)
 END
 GO
 
  


CREATE Proc SpRegisterUser
(
@EmailId varchar(100),
@Password varchar(100),
@ContactNo varchar(100),
@District varchar(100),
@IsPolice int
)
AS 
BEGIN
DECLARE @Count INT=0;
Select @Count=COUNT(EmailId) from CrimeUsers Where EmailId=@EmailId 

IF  @Count>0
 
	SELECT 'User already exists' As  Response;

 
ELSE

BEGIN
  INSERT INTO CrimeUsers (EmailId,[Password],ContactNo,District,IsPolice)
  VALUES (@EmailId,@Password,@ContactNo,@District,@IsPolice)

  SELECT 'User registered successfully' As  Response;
END



END

GO


 

 CREATE Procedure [dbo].[spAuthenticateUser]
@UserName nvarchar(100),
@Password nvarchar(100)
as
Begin
 Declare @Count int
 
 Select @Count = COUNT(*) from CrimeUsers
 where EmailId = @UserName and [Password] = @Password
 
 if(@Count = 1)
 Begin
  Select 1 as ReturnCode
 End
 Else
 Begin
  Select -1 as ReturnCode
 End
End
GO