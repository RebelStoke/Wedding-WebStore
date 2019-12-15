import {Component, ComponentFactoryResolver, OnInit} from '@angular/core';


@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent implements OnInit {
  Images1 = [];
  set: number;

  constructor(private resolver: ComponentFactoryResolver) {
  }

  ngOnInit(): void {
    this.loadPictures(0);
    this.set = 0;
  }

  loadPictures(set): void {
    this.Images1 = [];
   for (let i = 0; i < 9; i++) {
      const path = '../../assets/gallery/' + set + '/' + i + '.jpg';
      this.Images1.push(path);
    }
  }

  prevSet(): void {
    if (this.set > 0) {
      this.set--;
      this.loadPictures(this.set);
    }

  }

  nextSet(): void {
    if (this.set < 1) {
      this.set++;
      this.loadPictures(this.set);
    }
  }

  openCarousel() {
    document.getElementById('carouselWrapper').style.display = 'flex';
    document.getElementById('carouselWrapper').style.position = 'fixed';
  }

  closeCarousel() {
    document.getElementById('carouselWrapper').style.display = 'none';
  }

  imageErrorHandler($event) {
    let image = $event.target;
    image.style.display = 'none';
    this.removeImageFromArray(image.getAttribute("src"));
  }

  removeImageFromArray(src){
    let index = this.Images1.indexOf(src);
    console.log(src);
    if(index > -1)
      this.Images1.splice(index, 1);
  }
}



