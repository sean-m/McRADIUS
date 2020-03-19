using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace McRADIUS.Models
{
    public class OTPRequest
    {
        private readonly string _username;

        public OTPRequest() { }

        public OTPRequest(string Username)
        {
            this._username = Username;
            this.Status = RequestStatus.Pending;
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime ValidUntil { get; set; }

        public RequestStatus Status { get; set; }

        public string Username { get => _username; }

        public string SetOTPContactAddress { get; set; } = String.Empty;
    }

    public enum RequestStatus {
        Pending,
        Sent,
        Expired,
        Validated
    }
}
