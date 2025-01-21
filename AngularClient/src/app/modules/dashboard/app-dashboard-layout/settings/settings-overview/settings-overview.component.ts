import { Component } from '@angular/core';

@Component({
  selector: 'app-settings-overview',
  standalone: false,
  templateUrl: './settings-overview.component.html',
  styleUrl: './settings-overview.component.scss'
})
export class SettingsOverviewComponent {  settings = {
  blogTitle: '',
  blogDescription: '',
  blogLogo: '',
  themeColor: '#000000', // Default color
  enableComments: true,
  socialMediaLinks: {
    facebook: '',
    twitter: '',
    instagram: ''
  }
};

logoPreview: string | ArrayBuffer | null = null;

onLogoChange(event: Event): void {
  const file = (event.target as HTMLInputElement).files?.[0];
  if (file) {
    const reader = new FileReader();
    reader.onload = () => {
      this.logoPreview = reader.result;
      this.settings.blogLogo = reader.result as string;
    };
    reader.readAsDataURL(file);
  }
}

onSubmit(form: any): void {
  if (form.valid) {
    console.log('Settings saved:', this.settings);
    // Call an API or service to save the settings
  } else {
    console.log('Form is invalid');
  }
}
}
