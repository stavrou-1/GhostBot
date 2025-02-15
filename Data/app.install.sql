drop table if exists "Persons";
drop table if exists "Comments";
drop table if exists "Categories";

create table "Persons" (
    "PersonId" nchar (5) primary key,
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
    "Notes" text null
);

create table "Comments" (
    "CommentId" integer primary key,
    "Content" text not null,
    "PersonId" integer null,
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

create table "Categories" (
    "CategoryId" integer primary key,
    "CategoryName" nvarchar(20) not null,
    "Description" text null,
    "Picture" "image" null
);

create index "PersonId" on "Comments"("PersonId");
create index "CategoryId" on "Categories"("categoryId");
create index "CommentId" on "Comments"("CommentId");

insert into "Categories"("CategoryName","Description","Picture") 
values ("AI",null,null);

insert into "Categories"("CategoryName","Description","Picture") 
values ("Politics",null,null);

insert into "Categories"("CategoryName","Description","Picture") 
values ("Sports",null,null);

insert into "Categories"("CategoryName","Description","Picture") 
values ("Engineering",null,null);

insert into "Categories"("CategoryName","Description","Picture") 
values ("Philosophy",null,null);

insert into "Categories"("CategoryName","Description","Picture") 
values ("Food",null,null);

insert into "Categories"("CategoryName","Description","Picture") 
values ("Science",null,null);

insert into "Comments"(
    "Content",
    "PersonId",
    "ParentId",
    "CategoryId",
    "CreatedAt",
    "UpdatedAt",
    "DeletedAt",
    "CommentDate",
    "CommentReplies",
    "Category")
values
("Test comment"
,1
,1    
,1
,date('now')
,date('now')
,date('now')
,date('now')
,""
,1);

insert into "Persons"("FirstName","LastName","Avatar","Comments","Address"
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


insert into "Persons"("FirstName","LastName","Avatar","Comments","Address"
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