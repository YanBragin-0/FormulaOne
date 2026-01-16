import { JsonPipe } from '@angular/common';
import { Component, signal,inject,OnInit, computed } from '@angular/core';
import { RouterOutlet,RouterLink } from '@angular/router';
import { ConstrCshService } from './services/constr-csh-service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, MatToolbarModule, MatButtonModule],
  templateUrl: './app.html',
  styleUrls: ['./app.scss'],
})
export class App  {
    readonly videos = [
      '/videos/img-6226.mp4',
      '/videos/img-6227_7QPweXYv.mp4',
      '/videos/img-6228_upLKHz4A.mp4'
    ];
    private currentIdx = 0;
  
    public currentVideo = signal(this.videos[0]);

    public playNext() {
      this.currentIdx = (this.currentIdx + 1) % this.videos.length;
      this.currentVideo.set(this.videos[this.currentIdx]);
  }
}
