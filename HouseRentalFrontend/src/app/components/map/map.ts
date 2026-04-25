import { Component, AfterViewInit, Output, EventEmitter, input, effect, ChangeDetectorRef } from '@angular/core';
import * as L from 'leaflet';
import 'leaflet-control-geocoder';
import { NgIf,DecimalPipe } from '@angular/common';

delete (L.Icon.Default.prototype as any)._getIconUrl;

L.Icon.Default.mergeOptions({
  iconRetinaUrl: '/marker-icon-2x.png',
  iconUrl: '/marker-icon.png',
  shadowUrl: '/marker-shadow.png'
});

@Component({
  selector: 'app-map',
  imports: [NgIf,DecimalPipe],
  templateUrl: './map.html',
  styleUrl: './map.css',
})
export class Map implements AfterViewInit {
  private map!: L.Map;
  private selectedMarker: L.Marker | null = null;
  private routeLayer: L.GeoJSON | null = null;
  userMarker: L.Marker | null = null;
  propertyMarker: L.Marker | null = null;
  distance:number=0;

  @Output() selectedLatLng = new EventEmitter<{ lat: number, lng: number } | null>();
  propertyLat = input<number | null>(null);
  propertyLng = input<number | null>(null);

  newLat:number|null=null;
  newLng:number|null=null;


  constructor(private cdr:ChangeDetectorRef) {
    effect(() => {
      this.updatePropertyMarker();
    });
  }



  giveCoords() {
    if (this.selectedMarker) {
      const latlng = this.selectedMarker.getLatLng();

      this.selectedLatLng.emit({
        lat: latlng.lat,
        lng: latlng.lng
      });
    } else {
      this.selectedLatLng.emit(null);
    }
  }

  locateUser() {

    if (!navigator.geolocation) {
      alert("Geolocation not supported by your browser");
      return;
    }

    navigator.geolocation.getCurrentPosition(
      (position) => {

        const lat = position.coords.latitude;
        const lng = position.coords.longitude;

        console.log("User location:", lat, lng);

        this.map.setView([lat, lng], 16);

        if (this.userMarker) {
          this.map.removeLayer(this.userMarker);
        }

        this.userMarker = L.marker([lat, lng])
          .addTo(this.map)
          .bindPopup("You are here")
          .openPopup();

        if(this.selectedMarker){
          this.map.removeLayer(this.selectedMarker);
        }

        this.newLat=lat;
        this.newLng=lng;

        this.selectedMarker=L.marker([lat, lng]).addTo(this.map).bindPopup("You are here").openPopup();

      },
      (error) => {
        console.error(error);
        alert("Unable to fetch location. Please allow permission.");
      }
    );
  }

  ngAfterViewInit(): void {
    this.initMap();
    this.updatePropertyMarker();
  }



  private updatePropertyMarker(): void {
    const lat = this.propertyLat();
    const lng = this.propertyLng();

    if (this.map && lat != null && lng != null) {
      if (this.propertyMarker) {
        this.map.removeLayer(this.propertyMarker);
      }
      this.propertyMarker = L.marker([lat, lng]).addTo(this.map).bindPopup("Property Location");
      this.map.setView([lat, lng], 15);
    }
  }

  private initMap(): void {

    this.map = L.map('map').setView([27.7172, 85.3240], 13);


    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(this.map);

    (L.Control as any).geocoder({ defaultMarkGeocode: true }).addTo(this.map);


    this.map.on('click', (e: L.LeafletMouseEvent) => {
      const lat = e.latlng.lat;
      const lng = e.latlng.lng;

      if (this.selectedMarker) {
        this.map.removeLayer(this.selectedMarker);
      }

      if(this.userMarker){
        this.map.removeLayer(this.userMarker);
      }

      this.selectedMarker = L.marker([lat, lng]).addTo(this.map);
      this.giveCoords();

      const propLat = this.propertyLat();
      const propLng = this.propertyLng();

      this.newLat=lat;
      this.newLng=lng;
      
    });
  }

  showRoute(){
    if(this.newLat==null || this.newLng==null || this.propertyLat()==null || this.propertyLng()==null){
      alert("Unable to find location for adding property");
      return;
    }
    
      const propLat=this.propertyLat();
      const propLng=this.propertyLng();
      
      this.getRoute(this.newLat!,this.newLng!,propLat!,propLng!);
  }




  getRoute(startLat: number, startLng: number, endLat: number, endLng: number) {

    fetch(`https://router.project-osrm.org/route/v1/driving/${startLng},${startLat};${endLng},${endLat}?overview=full&geometries=geojson`)
      .then(res => res.json())
      .then(data => {

        const route = data.routes[0].geometry;

        if (this.routeLayer) {
        this.map.removeLayer(this.routeLayer);
      }

        this.routeLayer = L.geoJSON(route).addTo(this.map);

        const distance = data.routes[0].distance / 1000;
        this.distance=distance;
        this.cdr.detectChanges();

        const bounds = this.routeLayer.getBounds();
        this.map.fitBounds(bounds, { padding: [50, 50] });

      });

  }
}
