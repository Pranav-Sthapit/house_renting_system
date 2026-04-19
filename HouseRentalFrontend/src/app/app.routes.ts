import { Routes } from '@angular/router';
import { Register } from './components/register/register';
import { Login } from './components/login/login';
import { OwnerHome } from './components/owner/owner-home/owner-home';
import { RenterHome } from './components/renter/renter-home/renter-home';
import { AddProperty } from './components/owner/add-property/add-property';
import { ViewProperty } from './components/owner/view-property/view-property';
import { OwnerPropertyDetail } from './components/owner/owner-property-detail/owner-property-detail';
import { ViewRenters } from './components/owner/view-renters/view-renters';
import { RenterDetailsByOwner } from './components/owner/renter-details-by-owner/renter-details-by-owner';
import { OwnerProfile } from './components/owner/owner-profile/owner-profile';
import { RenterProfile } from './components/renter/renter-profile/renter-profile';
import { RenterViewRentals } from './components/renter/renter-view-rentals/renter-view-rentals';
import { RenterViewProperty } from './components/renter/renter-view-property/renter-view-property';
import { RenterViewPropertyDetails } from './components/renter/renter-view-property-details/renter-view-property-details';
import { RentalAndPropertyDetails } from './components/renter/rental-and-property-details/rental-and-property-details';
import { LandingPage } from './components/landing-page/landing-page';

export const routes: Routes = [
    { path: "", component: LandingPage },
    { path: 'register', component: Register },
    { path: 'login', component: Login },
    {
        path: 'owner-home', component: OwnerHome,
        children: [
            { path: 'add-property', component: AddProperty },
            { path: 'view-property', component: ViewProperty },
            { path: 'owner-property-detail/:id', component: OwnerPropertyDetail },
            { path: 'view-renters/:id', component: ViewRenters },
            { path: 'renter-details-by-owner/:propertyId/:renterId', component: RenterDetailsByOwner },
            { path: 'owner-profile', component: OwnerProfile }
        ]
    },
    {
        path: 'renter-home', component: RenterHome,
        children: [
            { path: 'view-property', component: RenterViewProperty },
            { path: 'view-rentals', component: RenterViewRentals },
            { path: 'renter-profile', component: RenterProfile },
            { path: 'renter-property-details/:id', component: RenterViewPropertyDetails },
            { path: 'rental-and-property-details/:propertyId', component: RentalAndPropertyDetails }
        ]
    },

];
