export type LoginDTO = {
    username: string;
    password: string;
}

export type RegisterDTO = {
    firstName: string;
    lastName: string;
    address: string;
    email: string;
    contact: string;
    username: string;
    password: string;
    profilePhoto: File | null;
}