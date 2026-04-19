export type RentalResponseForOwnerDTO = {
    renterId: number;
    firstName: string;
    lastName: string;
    contact: string;
    tenant: string;
    proposedTenant: string;
    rent: number;
    proposedRent: number;
    status: string;
}

export type RentalResponseForOwnerWithDetailsDTO = RentalResponseForOwnerDTO & {
    address: string;
    email: string;
    profilePhoto: string;
    passport: string;
    citizenship: string;
}