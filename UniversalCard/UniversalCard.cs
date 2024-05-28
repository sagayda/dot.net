namespace UniversalCard;

public class UniversalCard : IBankCard, IPassport, IStudentID, IInsurancePolicy, IDriverLicense
{
	private readonly BankCard _bankCard;
	private readonly Passport _passport;
	private readonly StudentID _studentID;
	private readonly InsurancePolicy _insurancePolicy;
	private readonly DriverLicense _driverLicense;

	public UniversalCard(BankCard bankCard, Passport passport, StudentID studentID, InsurancePolicy insurancePolicy, DriverLicense driverLicense)
	{
		_bankCard = bankCard;
		_passport = passport;
		_studentID = studentID;
		_insurancePolicy = insurancePolicy;
		_driverLicense = driverLicense;
	}

	#region IBankCard Members
	string IBankCard.Bank => _bankCard.Bank;

	string IBankCard.GetPaymentDetails()
	{
		return _bankCard.GetPaymentDetails();
	}

	bool IBankCard.IsValid()
	{
		return _bankCard.IsValid();
	}
	#endregion

	#region IPassport Members
	Person IPassport.Holder => _passport.Holder;
	
	int IPassport.GetHolderAge()
	{
		return _passport.GetHolderAge();
	}
	
	bool IPassport.IsValid()
	{
		return _passport.IsValid();
	}
	#endregion

	#region IStudentID Members
	string IStudentID.University => _studentID.University;

	string IStudentID.Faculty => _studentID.Faculty;
	
	bool IStudentID.IsValid()
	{
		return _studentID.IsValid();
	}
	#endregion

	#region IInsurancePolicy Members
	string IInsurancePolicy.Provider => _insurancePolicy.Provider;
	
	bool IInsurancePolicy.IsValid()
	{
		return _insurancePolicy.IsValid();
	}
	#endregion

	#region IDrivingLicense Members
	bool IDriverLicense.IsValid()
	{
		return _driverLicense.IsValid();
	}
	#endregion
}
