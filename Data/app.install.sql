drop table if exists "Person";

create table "Person" (
    "PersonId" integer primary key,
    "FirstName" nvarchar (50) not null,
    "LastName" nvarchar (50) not null,
    "Avatar" "image" null,
    "Comments" nvarchar (50) null,
    "Address" nvarchar (60) null,
    "City" nvarchar (15) null,
    "Region" nvarchar (15) null,
    "PostalCode" nvarchar (10) null,
    "Country" nvarchar (25) null,
    "AuthenticationLevel" nvarchar (10) null,
    "IsSSO" bit default 0,
    "AnonymousPosts" integer null, 
    "EtherealThreads" integer null,
    "Notes" "ntext" null
);

create table "Comment" (
    "CommentId" integer primary key,
    "Comment" "ntext" not null,
    "PersonId" nchar (5) null,
    "ParentId" integer null,
    "CategoryId" integer null,
    "CreatedAt" "datetime" null,
    "UpdatedAt" "datetime" null,
    "DeletedAt" "datetime" null,
    "CommentDate" "datetime" null,
    "CommentReplies" nvarchar(50) null,
    "Category" integer null ,
    constraint "FK_Comment_Person" foreign key (
        "PersonId"
    ) references "Person" (
        "PersonId"
    ),
    constraint "FK_Comment_Category" foreign key (
        "CategoryId"
    ) references "Category" (
        "CategoryId"
    )
);

create table "Category" (
    "CategoryId" integer primary key,
    "CategoryName" nvarchar(20) not null,
    "Description" "ntext" null,
    "Picture" "image" null
);

create index "PersonId" on "Comment"("PersonId");
create index "CategoryId" on "Category"("categoryId");

insert into "Category"("CategoryName","Description","Picture") 
values ("AI",null,null);

insert into "Category"("CategoryName","Description","Picture") 
values ("Politics",null,null);

insert into "Category"("CategoryName","Description","Picture") 
values ("Sports",null,null);

insert into "Category"("CategoryName","Description","Picture") 
values ("Engineering",null,null);

insert into "Category"("CategoryName","Description","Picture") 
values ("Philosophy",null,null);

insert into "Category"("CategoryName","Description","Picture") 
values ("Food",null,null);

insert into "Category"("CategoryName","Description","Picture") 
values ("Science",null,null);


insert into "Person"("FirstName","LastName","Avatar","Comments","Address"
,"City","Region","PostalCode","Country","AuthenticationLevel","IsSSO"
,"AnonymousPosts","EtherealThreads","Notes")
values
("admin"
,"admin"
,null
,null
,"123 Cyber Way"
,"Ether"
,"Realm"
,null
,null
,null
,"root"
,null
,null
,null);


insert into "Person"("FirstName","LastName","Avatar","Comments","Address"
,"City","Region","PostalCode","Country","AuthenticationLevel","IsSSO"
,"AnonymousPosts","EtherealThreads","Notes")
values
("John"
,"Acropolis"
,null
,null
,"123 Parthenon Way"
,"Athens"
,"Greece"
,null
,null
,null
,"commenter"
,null
,null
,null);