export const validateName=(name:string,firstOrLast:string):string|null=>{
    if(name.length==0){
        return `${firstOrLast} is required`;
    }
    const regex=/^[a-zA-Z]+$/;
    if(regex.test(name)){
        return null;
    }
    return `${firstOrLast} must contain only alphabets`;
}

export const validateUsername=(username:string):string|null=>{
    if(username.length==0){
        return "Username is required";
    }
    if(username.length<3 || username.length>20){
        return "Username must be between 6-20 characters long";
    }
    const regex=/^[a-zA-Z0-9]+$/;
    if(regex.test(username)){
        return null;
    }
    return "Username must contain only alphabets and numbers";
}

export const validatePassword=(password:string):string|null=>{
    if(password.length<6 || password.length>20){
        return "Password must be between 6-20 characters long";
    }
    const regex=/^\S+$/;
    if(regex.test(password)){
        return null;
    }
    return "Password must not contain spaces";
}

export const validateContact=(contact:number):string|null=>{
    if(contact.toString().length==0){
        return "Contact is required";
    }
    const regex=/^[9][0-9]{9}$/;
    if(regex.test(contact.toString())){
        return null;
    }
    return "Contact must start with 9 and be 10 digits long";
}

export const validateAddress=(address:string):string|null=>{
    if(address.length==0){
        return "Address is required";
    }
    
    return null;
}