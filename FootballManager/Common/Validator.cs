using FootballManager.Data;
using FootballManager.ViewModels.Players;
using FootballManager.ViewModels.Users;
using System;
using System.Text.RegularExpressions;

namespace FootballManager.Common
{
    using static DataConstants;
    public class Validator : IValidator
    {
        private string emailRegex =
            @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

        public bool ValidateUser(RegisterUserFormModel model)
        {
            bool isValid = true;

            if (NullOrWhiteSpace(model.Username))
            {
                isValid = false;
            }

            if (model.Username.Length < UsernameMinLength ||
               model.Username.Length > UsernameMaxLength)
            {
                isValid = false;
            }

            if (NullOrWhiteSpace(model.Email))
            {
                isValid = false;
            }

            if (!Regex.IsMatch(model.Email, emailRegex))
            {
                isValid = false;
            }

            if (model.Email.Length < EmailMinLength ||
               model.Email.Length > EmailMaxLength)
            {
                isValid = false;
            }

            if (NullOrWhiteSpace(model.Password) ||
               NullOrWhiteSpace(model.ConfirmPassword))
            {
                isValid = false;
            }

            if (model.Password.Length < PasswordMinLength ||
               model.Password.Length > PasswordMaxLength)
            {
                isValid = false;
            }

            if (model.Password != model.ConfirmPassword)
            {
                isValid = false;
            }

            return isValid;
        }
        public bool ValidatePlayer(AddPlayerFormModel model)
        {
            bool isValid = true;   

            if(NullOrWhiteSpace(model.FullName))
            {
                isValid=false;
            }

            if(model.FullName.Length < FullNameMinLength ||
               model.FullName.Length > FullNameMaxLength)
            {
                isValid = false;
            }

            if(NullOrWhiteSpace(model.ImageUrl))
            {
                isValid = false;
            }

            if(!Uri.IsWellFormedUriString(model.ImageUrl, UriKind.Absolute))
            {
                isValid = false;
            }

            if(NullOrWhiteSpace(model.Position))
            {
                isValid = false;
            }

            if(model.Position.Length < PositionMinLength ||
               model.Position.Length > PositionMaxLength)
            {
                isValid = false;
            }

            if (model.Speed < SpeedMinValue ||
                model.Speed > SpeedMaxValue)
            {
                isValid = false;
            }

            if(model.Endurance < EnduranceMinValue ||
                model.Endurance > EnduranceMaxValue)
            {
                isValid = false;
            }

            if (NullOrWhiteSpace(model.Description))
            {
                isValid = false;
            }

            if(model.Description.Length > DescriptionMaxLength)
            {
                isValid = false;
            }

            return isValid;
        }
        private bool NullOrWhiteSpace(string parameter)
        => (string.IsNullOrEmpty(parameter) ||
            string.IsNullOrWhiteSpace(parameter));
    }
}
