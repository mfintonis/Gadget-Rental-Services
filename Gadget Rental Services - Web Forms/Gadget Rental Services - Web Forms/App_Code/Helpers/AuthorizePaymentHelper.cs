using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace App_Code.Helpers
{
    public class AuthorizePaymentHelper
    {
        private creditCardType _creditCard { get; set; }
        private customerAddressType _billingAddress { get; set; }
        public customerAddressType BillingAddress { get { return _billingAddress; } }
        private List<lineItemType> _itemsBeingSold { get; set; }
        private orderType _order;
        private string _apiResponse { get; set; }
        private string _authCode { get; set; }

        public AuthorizePaymentHelper()
        {
            _itemsBeingSold = new List<lineItemType>();
            _apiResponse = "Too Early! Payment not processed yet";
        }

        public string ApiResponse
        {
            get
            {
                return _apiResponse;
            }
        }

        public string AuthCode
        {
            get
            {
                return _authCode;
            }
        }

        /// <summary>
        /// Set the credit card info that the helper will use to process a payment
        /// </summary>
        /// <param name="cardNumber">The card info with no dashes or other characters other than numbers</param>
        /// <param name="expirationDate">The expiration date in the format MMYY Ex: 0722</param>
        /// <param name="securityCode">The security code usually 3 or 4 digits long depeniding on the card company</param>
        public void SetCardInfo(string cardNumber, string expirationDate, string securityCode)
        {
            _creditCard = new creditCardType
            {
                cardNumber = cardNumber,
                expirationDate = expirationDate,
                cardCode = securityCode
            };
        }

        /// <summary>
        /// Set the nvoice number for the order
        /// </summary>
        /// <param name="invoiceNumber">Value used as invoice number</param>
        public void SetOrderInfo(string invoiceNumber)
        {
            _order = new orderType
            {
                invoiceNumber = invoiceNumber,
            };
        }

        /// <summary>
        /// Set the billing address for the customer
        /// </summary>
        public void SetBillingAddress(string firstName, string lastName, string address, string city, string zip, string email, string phoneNumber, string country, string state)
        {
            _billingAddress = new customerAddressType
            {
                firstName = firstName,
                lastName = lastName,
                address = address,
                city = city,
                zip = zip,
                email = email,
                phoneNumber = phoneNumber,
                country = country,
                state = state
            };
        }

        public static bool BillingAddressIsValid(customerAddressType BillingAddress)
        {
            return !string.IsNullOrWhiteSpace(BillingAddress.address) && !string.IsNullOrWhiteSpace(BillingAddress.city) && !string.IsNullOrWhiteSpace(BillingAddress.state) && !string.IsNullOrWhiteSpace(BillingAddress.zip) && !string.IsNullOrWhiteSpace(BillingAddress.country);
        }
        /// <summary>
        /// Adds an item being sold to the collection so the helper knows what is being bought
        /// </summary>
        /// <param name="item">lineItemType that you want to add to the Helpers internal collection of items</param>
        public void AddItemBeingSold(lineItemType item)
        {
            _itemsBeingSold.Add(item);
        }

        /// <summary>
        /// Process Transaction. Do this after setting all other parameters
        /// </summary>
        /// <param name="totalAmount">Total amount of the transaction</param>
        /// <returns>Returns a string representing the transaction id. Will return an empty string if transaction fails</returns>
        public string ProcessTransaction(decimal totalAmount, string name = null, string Item = null)
        {
            try
            {
                transactionRequestType transactionRequest;

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                bool useTestMode = true; //ValidationHelper.GetBoolean(SettingsKeyInfoProvider.GetValue("CMSAuthorizeNETTestMode", SiteContext.CurrentSiteID), true);

                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = useTestMode ? AuthorizeNet.Environment.SANDBOX : AuthorizeNet.Environment.PRODUCTION;


                if (String.IsNullOrWhiteSpace(name))
                {
                    name = "3FRZg6qWP2w";
                }

                if (String.IsNullOrWhiteSpace(Item))
                {
                    Item = "9B4fvr4zy2D6P6M3";
                }

                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = name,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = Item,
                };

                var paymentType = new paymentType { Item = _creditCard };


                if (_order != null && !String.IsNullOrEmpty(_order.invoiceNumber))
                {
                    transactionRequest = new transactionRequestType
                    {
                        transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),

                        amount = totalAmount,
                        payment = paymentType,
                        billTo = _billingAddress,
                        lineItems = _itemsBeingSold.ToArray(),
                        order = _order,
                    };
                }
                else
                {
                    transactionRequest = new transactionRequestType
                    {
                        transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),

                        amount = totalAmount,
                        payment = paymentType,
                        billTo = _billingAddress,
                        lineItems = _itemsBeingSold.ToArray(),
                    };
                }

                var request = new createTransactionRequest { transactionRequest = transactionRequest };
                var controller = new createTransactionController(request);

                controller.Execute();

                var response = controller.GetApiResponse();
                if (response != null)
                {
                    if (response.messages.resultCode == messageTypeEnum.Ok)
                    {
                        if (response.transactionResponse.messages != null)
                        {
                            _apiResponse = "Successfully created transaction with Transaction ID: " + response.transactionResponse.transId;
                            _apiResponse += "<br />Response Code: " + response.transactionResponse.responseCode;
                            _apiResponse += "<br />Message Code: " + response.transactionResponse.messages[0].code;
                            _apiResponse += "<br />Description: " + response.transactionResponse.messages[0].description;
                            _apiResponse += "<br />Success, Auth Code : " + response.transactionResponse.authCode;
                            _authCode = response.transactionResponse.authCode;
                            return response.transactionResponse.transId;
                        }
                        else
                        {
                            _apiResponse = "Failed Transaction.";
                            if (response.transactionResponse.errors != null)
                            {
                                _apiResponse += "<br />Error Code: " + response.transactionResponse.errors[0].errorCode;
                                _apiResponse += "<br />Error message: " + response.transactionResponse.errors[0].errorText;
                            }
                            return "";
                        }
                    }
                    else
                    {
                        _apiResponse = "Failed Transaction.";
                        if (response.transactionResponse.errors != null)
                        {
                            _apiResponse += "<br />Error Code: " + response.transactionResponse.errors[0].errorCode;
                            _apiResponse += "<br />Error message: " + response.transactionResponse.errors[0].errorText;
                            return "";
                        }
                        else
                        {
                            _apiResponse += "<br />Error Code: " + response.messages.message[0].code;
                            _apiResponse += "<br />Error message: " + response.messages.message[0].text;
                            return "";
                        }
                    }
                }
                else
                {
                    _apiResponse = "Response object was null! Usually occurs when invalid info (such as credit card number or security code) is passed to helper.";
                    return "";
                }
            }
            catch (Exception e)
            {
                _apiResponse = "Exception occured while processing the transaction. Exception Message: " + e.Message;
                return "";
            }
        }
    }
}