//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOAEmailSenderMonthly
{
    using System;
    using System.Collections.Generic;
    
    public partial class bil_SettlementCutOff
    {
        public long SettlementCutOffID { get; set; }
        public System.DateTime settlement_date { get; set; }
        public int merchant_id { get; set; }
        public int InternalCompanyID { get; set; }
        public int CorporateID { get; set; }
        public int AcquirerID { get; set; }
        public int settlement_batch_id { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal Transaction_GrossAmount { get; set; }
        public decimal Transaction_MerchantMDRAmount { get; set; }
        public decimal Transaction_MerchantVATAmount { get; set; }
        public decimal Transaction_MerchantWHTAmount { get; set; }
        public decimal Transaction_MerchantMDRWHTAmount { get; set; }
        public decimal Transaction_BankMDRAmount { get; set; }
        public decimal Transaction_BankVATAmount { get; set; }
        public decimal Transaction_BankWHTAmount { get; set; }
        public decimal Transaction_BankMDRWHTAmount { get; set; }
        public decimal Transaction_BankGrossAmountReceivable { get; set; }
        public decimal Transaction_BankGrossAmountReceivableWHTAmount { get; set; }
        public decimal Transaction_BankNetAmountReceivable { get; set; }
        public decimal Transaction_NetAmount { get; set; }
        public decimal Chargeback_GrossAmount { get; set; }
        public decimal Chargeback_MerchantMDRAmount { get; set; }
        public decimal Chargeback_MerchantVATAmount { get; set; }
        public decimal Chargeback_MerchantWHTAmount { get; set; }
        public decimal Chargeback_MerchantMDRWHTAmount { get; set; }
        public decimal Chargeback_BankMDRAmount { get; set; }
        public decimal Chargeback_BankVATAmount { get; set; }
        public decimal Chargeback_BankWHTAmount { get; set; }
        public decimal Chargeback_BankMDRWHTAmount { get; set; }
        public decimal Chargeback_BankGrossAmountReceivable { get; set; }
        public decimal Chargeback_BankGrossAmountReceivableWHTAmount { get; set; }
        public decimal Chargeback_BankNetAmountReceivable { get; set; }
        public decimal Chargeback_NetAmount { get; set; }
        public decimal Refund_GrossAmount { get; set; }
        public decimal Refund_MerchantMDRAmount { get; set; }
        public decimal Refund_MerchantVATAmount { get; set; }
        public decimal Refund_MerchantWHTAmount { get; set; }
        public decimal Refund_MerchantMDRWHTAmount { get; set; }
        public decimal Refund_BankMDRAmount { get; set; }
        public decimal Refund_BankVATAmount { get; set; }
        public decimal Refund_BankWHTAmount { get; set; }
        public decimal Refund_BankMDRWHTAmount { get; set; }
        public decimal Refund_BankGrossAmountReceivable { get; set; }
        public decimal Refund_BankGrossAmountReceivableWHTAmount { get; set; }
        public decimal Refund_BankNetAmountReceivable { get; set; }
        public decimal Refund_NetAmount { get; set; }
        public decimal TransactionAdjustment_GrossAmount { get; set; }
        public decimal TransactionAdjustment_MerchantMDRAmount { get; set; }
        public decimal TransactionAdjustment_MerchantVATAmount { get; set; }
        public decimal TransactionAdjustment_MerchantWHTAmount { get; set; }
        public decimal TransactionAdjustment_MerchantMDRWHTAmount { get; set; }
        public decimal TransactionAdjustment_BankMDRAmount { get; set; }
        public decimal TransactionAdjustment_BankVATAmount { get; set; }
        public decimal TransactionAdjustment_BankWHTAmount { get; set; }
        public decimal TransactionAdjustment_BankMDRWHTAmount { get; set; }
        public decimal TransactionAdjustment_BankGrossAmountReceivable { get; set; }
        public decimal TransactionAdjustment_BankGrossAmountReceivableWHTAmount { get; set; }
        public decimal TransactionAdjustment_BankNetAmountReceivable { get; set; }
        public decimal TransactionAdjustment_NetAmount { get; set; }
        public decimal AdjustmentAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal RemainingBalance { get; set; }
        public decimal Fraud_GrossAmount { get; set; }
        public decimal Fraud_MerchantMDRAmount { get; set; }
        public decimal Fraud_MerchantVATAmount { get; set; }
        public decimal Fraud_MerchantWHTAmount { get; set; }
        public decimal Fraud_MerchantMDRWHTAmount { get; set; }
        public decimal Fraud_BankMDRAmount { get; set; }
        public decimal Fraud_BankVATAmount { get; set; }
        public decimal Fraud_BankWHTAmount { get; set; }
        public decimal Fraud_BankMDRWHTAmount { get; set; }
        public decimal Fraud_BankGrossAmountReceivable { get; set; }
        public decimal Fraud_BankGrossAmountReceivableWHTAmount { get; set; }
        public decimal Fraud_BankNetAmountReceivable { get; set; }
        public decimal Fraud_NetAmount { get; set; }
        public decimal FraudRelease_GrossAmount { get; set; }
        public decimal FraudRelease_MerchantMDRAmount { get; set; }
        public decimal FraudRelease_MerchantVATAmount { get; set; }
        public decimal FraudRelease_MerchantWHTAmount { get; set; }
        public decimal FraudRelease_MerchantMDRWHTAmount { get; set; }
        public decimal FraudRelease_BankMDRAmount { get; set; }
        public decimal FraudRelease_BankVATAmount { get; set; }
        public decimal FraudRelease_BankWHTAmount { get; set; }
        public decimal FraudRelease_BankMDRWHTAmount { get; set; }
        public decimal FraudRelease_BankGrossAmountReceivable { get; set; }
        public decimal FraudRelease_BankGrossAmountReceivableWHTAmount { get; set; }
        public decimal FraudRelease_BankNetAmountReceivable { get; set; }
        public decimal FraudRelease_NetAmount { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool SettlementMovementFlag { get; set; }
        public bool SOASendEmail { get; set; }
        public Nullable<long> EmailSenderID { get; set; }
        public Nullable<int> account_bank { get; set; }
        public string account_bank_branch_name { get; set; }
        public string account_no { get; set; }
        public string account_name { get; set; }
        public string GIRO_merchantbankcode { get; set; }
        public string GIRO_bankcode { get; set; }
        public Nullable<bool> GIROEnabled { get; set; }
        public Nullable<byte> GIROStatus { get; set; }
        public Nullable<int> ADABankID { get; set; }
        public string ADAReferenceNo { get; set; }
        public string ADAmerchantbankcode { get; set; }
        public Nullable<byte> ADAStatus { get; set; }
        public Nullable<bool> ADAEnabled { get; set; }
        public Nullable<int> CollectionBankID { get; set; }
        public Nullable<decimal> CollectionBankADAFee { get; set; }
        public string CollectionBankAccount { get; set; }
    }
}
