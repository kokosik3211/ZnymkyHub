use ZnymkyHubDB;

delete from Chats;
delete from FavouritePhotographers;
delete from Savings;
delete from Comments;
delete from Likes;
delete from PhotographerOutgoingCities;
delete from OutgoingCities;
delete from PhotoResolutions;
delete from Photos;
delete from Photoshoots;
delete from UserPhotoshootTypes;
delete from PhotoshootTypes;

insert into PhotoshootTypes ([Name])
            values
                            ('love_story'),
                            ('wedding'),
                            ('pending'),
                            ('children'),
                            ('family'),
                            ('portrait'),
                            ('nude'),
                            ('newborn'),
                            ('reportage'),
                            ('landscape'),
                            ('food_photo'),
                            ('business'),
                            ('fashion'),
                            ('commercial_photo'),
                            ('travel');

insert into UserPhotoshootTypes (PhotographerId, PhotoshootTypeId, Price)
            values
                                (2, 2, 100),
                                (2, 6, 100),
                                (2, 7, 100),
                                
                                (3, 5, 110),
                                (3, 11, 110),
                                (3, 9, 110),
                                (3, 12, 110),
                                (3, 6, 110),

                                (4, 8, 200),
                                (4, 5, 200),

                                (5, 6, 200),
                                (5, 9, 150),

                                (6, 2, 80),
                                (6, 12, 80),
                                (6, 5, 80),
                                (6, 4, 80),
                                (6, 9, 80),

                                (7, 6, 100),
                                (7, 1, 100),
                                (7, 2, 100),

                                (8, 9, 150),
                                (8, 14, 150),

                                (9, 1, 300),
                                (9, 2, 600),

                                (10, 7, 50),

                                (11, 13, 400),

                                (12, 15, 500),
                                (12, 9, 240),

                                (13, 10, 200),
                                (13, 11, 100),

                                (14, 2, 500);

insert into Photoshoots (PhotographerId, [Name], [Description], PhotoshootTypeId)
            values
                        (9, 'PAMIR', 'This is an expedition to the farthest corner of the Carpathians where at the top it is fascinating with its secrecy and mysticism - PAMIR', 1);

insert into Photos ([Name], PhotographerId, PhotoshootId, [DateTime], NumberOfLikes, NumberOfSaving)
            values
                        ('1', 9, 1, '2016-06-03 08:30:20', 0, 0),
                        ('2', 9, 1, '2016-06-03 08:30:56', 0, 0),
                        ('3', 9, 1, '2016-06-03 08:31:13', 0, 0),
                        ('4', 9, 1, '2016-06-03 08:32:03', 0, 0),
                        ('5', 9, 1, '2016-06-03 08:32:46', 0, 0),
                        ('6', 9, 1, '2016-06-03 08:33:14', 0, 0),                
                        ('7', 9, 1, '2016-06-03 08:34:49', 0, 0),
                        ('8', 9, 1, '2016-06-03 08:35:40', 0, 0),
                        ('9', 9, 1, '2016-06-03 08:36:27', 0, 0),
                        ('10', 9, 1, '2016-06-03 08:37:38', 0, 0),
                        ('11', 9, 1, '2016-06-03 08:38:21', 0, 0),
                        ('12', 9, 1, '2016-06-03 08:39:59', 0, 0),
                        ('13', 9, 1, '2016-06-03 08:41:42', 0, 0),
                        ('14', 9, 1, '2016-06-03 08:45:46', 0, 0),
                        ('15', 9, 1, '2016-06-03 08:47:29', 0, 0);