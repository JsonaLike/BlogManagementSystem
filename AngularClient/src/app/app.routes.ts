import { Routes } from '@angular/router';

export const routes: Routes = [
    {
		path: 'dashboard',
		loadChildren: () =>
			import('./modules/dashboard/dashboard.module').then(
				m => m.DashboardModule,
			),
	},{
		path: '',
		loadChildren: () =>
			import('./modules/main/main.module').then(
				m => m.MainModule,
			),
	},
];
