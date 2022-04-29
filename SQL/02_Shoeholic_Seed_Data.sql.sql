USE [Shoeholic];
GO

set identity_insert [UserProfile] on;
insert into UserProfile (Id,  FirstName, LastName, Email, FirebaseUserProfileId) 
values (1, 'Foo', 'Barington', 'bob@gmail.com', 'LWFbv7RWUvPIt09FtfG69FV5S4i1');
insert into UserProfile (Id, FirstName, LastName, Email, FirebaseUserProfileId) 
values (2, 'Red', 'Do', 'jasmine@gmail.com', 'MY8C9joaV9MakYaKq3PyaeUn6M02');
set identity_insert [UserProfile] off;

set identity_insert [Collection] on;
insert into [Collection] ([Id], [Name], [UserProfileId]) 
values (1, 'Technology',1), (2, 'Politics',1), (3, 'Science',2);
set identity_insert [Collection] off;

set identity_insert [Tag] on;
insert into [Tag] ([Id], [Name])
values (1, 'Hightops'), (2, 'running'), (3, 'casual'), (4, 'rare');
set identity_insert [Tag] off;

set identity_insert [Brand] on;
insert into [Brand] ([Id], [Name])
values (1, 'Hightops'), (2, 'running'), (3, 'casual'), (4, 'rare');
set identity_insert [Brand] off;

set identity_insert [Shoe] on;
insert into [Shoe] ([Id], [Name], [BrandId],  [ReleaseDate], [RetailPrice], [PurchaseDate], [Title], [ColorWay], [CollectionId]) 
values (1, 'Jordan 1', 1,  '2019-04-01', 170, '2019-12-04', 'Court Purple', 'Purple, White, Black', 1);
set identity_insert [Shoe] off;

set identity_insert [ShoeTag] on;
insert into [ShoeTag] ([Id], [ShoeId], [TagId])
values (1, 1,1), (2, 1,2), (3, 1,3), (4, 1,4);
set identity_insert [ShoeTag] off;