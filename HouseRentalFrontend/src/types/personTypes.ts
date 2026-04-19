export type PersonDTO = {
    id: number;
    firstName: string;
    lastName: string;
    address: string;
    email: string;
    contact: string;
    username: string;
    password: string;
}

export type RenterUpdateDTO = PersonDTO & {
    profilePhoto: File;
    citizenship: File;
    passport: File;
}

export type RenterResponseDTO = PersonDTO & {
    profilePhoto: string | null;
    citizenship: string | null;
    passport: string | null;
}

export type OwnerUpdateDTO = PersonDTO & {
    profilePhoto: File;
}

export type OwnerResponseDTO = PersonDTO & {
    profilePhoto: string;
}