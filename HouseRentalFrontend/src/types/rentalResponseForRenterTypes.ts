export type RentalResponseForRenterDTO={
    propertyId:number;
    BHK:number;
    size:number;
    floor:string;
    locality:string;
    city:string;
    thumbnail:string;
    status:string;
}

export type RentalResponseForRenterWithDetailsDTO=RentalResponseForRenterDTO & {
    areaType:string;
    furnishingStatus:string;
    tenant:string;
    proposedTenant:string;
    rent:number;
    proposedRent:number;
    bathroom:number;
    aggrementOfTerms:string;
}

export type RentalRequestAndUpdateDTO={
    tenant:string;
    rent:number;
}