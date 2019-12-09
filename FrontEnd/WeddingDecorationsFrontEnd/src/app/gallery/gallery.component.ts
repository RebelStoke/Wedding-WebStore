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
      let img = new Image();
      img.src = '../../assets/gallery/' + set + '/' + i + '.jpg';
      if (img.width != 0)
        this.Images1.push('../../assets/gallery/' + set + '/' + i + '.jpg');
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
}


