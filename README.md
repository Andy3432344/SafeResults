# SafeResults

# A collection of tools for dealing with nulls, failures and the generic type issues that arise in this domain.

### `IResult`
This interface serves as the basis for the following interfaces which can be used to provide a success/fail response:

	- IFail
		Indicates the result was not successful, provides a string `Message` property
	- IOk
		Indicates Success
	- IOkBut
		Inherits from IOk
		Indicates the successful idempotency of some function, 
		but contains extra information in a `Message` property

### `IResult<T>`
Inherits from IResult, use of this interface indicates the Type of the intended return value and serves as the basis for several others interfaces:
	
	- IFail<T>
		Inherits from IFail and IResult<T>
	- IOk<T> 
		Inherits from IOk and IResult<T>
  		Provides a T `Value` property
	- IOkBut<T>
		Inherits from IOkBut and IOk<T>
	
## How to return Result objects
  
`using static SafeResults.ResultExtensions;` <br>
<sub>The using directive above is required for the helper methods used below to be in scope.</sub>
```
public IResult<decimal> GetCurrentPrice()
{
	try
	{
		decimal price = outsideAPI.GetMarketPrice(); //outside API could fail
		return ok(price);//everything is ok
	}
	catch(Exception ex)
	{
		//when there's an error you can return the error message
		//since the return type can't be inferred it must be declared,
  		//as `<decimal>` in this case
		return error<decimal>(ex.Message); 
	}
}
```
```
public IResult<int> somFunc(int x)
{
	var ret = someCalculation(x);
	if(someRequirement(ret))
	{
		return ok(ret);
	}
	else
	{
		return error<int>("someRequirement was false");
	}
}
```

<br>

## Handling Returned Results


### The convensional usage pattern is:

 ```
IResult<int> result = someFunc(x);

if(result is IOK<int> r)
{
	int someInt = r.Value;
	//use someInt for something...
}
else
{
	//handle failure
}
```

### However, having to check and cast the result every time can be combersome...<br>  

## Using `OptionResult<T>` and the `option` method

The OptionResult<T> class is a special implementation of the IResult<T> interface (`IOptionResult<T> : IResult<T>`). Using this implementation we can rewrite the above as:
```
var opt = option(0); //Here, the option result has a default value of 0

int something = opt | someFunc(x);
 //this can be read as: "int something equals option result OR somFunc of x"

//the `something` variable can now be used...
//But we don't know if someFunc was successful or not!

//we can check:

if(opt.Some)
{//It Was successful!
	//continue
}
else  // if(opt.None)
{//It failed :/
	//handle failure
	log.Error(opt.Message);
}
```
### A particular situation may allow a calculation to fail but require some *neutral* option result value.  In that case you can skip the check entirely(!):
```
var opt = option(1.0); //Here, the option result has a default value of 1

var r = opt | distance(this, that);

return r * Math.PI * 2;//won't return 0 if the `distance` method fails
```
### This can be made even shorter because we don't need the option result object:
```
 var r = option(1.0) | distance(this, that);
 return r * Math.PI * 2;
```
:)
