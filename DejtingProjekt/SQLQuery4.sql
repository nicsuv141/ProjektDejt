select FId, LastName, UserModels.UserID, Friends.UserID,FirstName,FriendId, RequestAccepted from UserModels join Friends on Friends.FId = UserModels.UserID

select FId, LastName, UserModels.UserID, Friends.UserID,FirstName,FriendId, RequestAccepted from UserModels join Friends on Friends.UserID = UserModels.UserID

update Friends
set RequestAccepted = 'false'
where FriendID = 1

select FriendId,FirstName from UserModels join Friends on Friends.FId = UserModels.UserID where RequestAccepted = 'false'
