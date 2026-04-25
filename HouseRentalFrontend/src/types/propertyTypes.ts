export type PropertyDTO = {
    bhk: number;
    rent: number;
    size: number;
    floor: string;
    areaType: string;
    locality: string;
    city: string;
    furnishingStatus: string;
    tenant: string;
    bathroom: number;
    latitude: number;
    longitude: number;
}

export type PropertyRequestDTO = PropertyDTO & {
    thumbnail: File;
    aggrementOfTerms: File;
    pictures?: File[];
}

export type PropertyResponseDTO = PropertyDTO & {
    id: number;
    thumbnail: string;
    aggrementOfTerms: string;
    ownerId: number;
    pictures?: string[];
}

export type PropertyUpdateDTO = PropertyDTO & {
    thumbnail?: File;
    aggrementOfTerms?: File;
    pictures?: File[];
}