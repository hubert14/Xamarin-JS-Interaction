﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using PetProject.Activities;
using PetProject.Common.Interfaces;
using PetProject.Common.Models;
using PetProject.Common.Services;

namespace PetProject.Presenters
{
    public class MainPresenter
    {
        private Activity _view;
        private IUserService _service;

        public MainPresenter(Activity view)
        {
            _view = view;
            var button = _view.FindViewById<Button>(Resource.Id.confirmButton);
            button.Click += OnConfirm;
            _service = new UserService();
        }

        private void OnConfirm(object sender, EventArgs eventArgs)
        {
            var userName = _view.FindViewById<EditText>(Resource.Id.userName);
            var radioGroup = _view.FindViewById<RadioGroup>(Resource.Id.main_radio_group);

            if (string.IsNullOrWhiteSpace(userName.Text) || radioGroup.CheckedRadioButtonId == -1) return;

            var user = new UserModel() { UserName = userName.Text };
            _service.Create(user);

            var intent = radioGroup.CheckedRadioButtonId == Resource.Id.main_radio_flappy
                ? new Intent(_view, typeof(FlappyActivity))
                : new Intent(_view, typeof(RaceActivity));

            intent.PutExtra("userName", userName.Text);

            _view.StartActivity(intent);
        }
    }
}