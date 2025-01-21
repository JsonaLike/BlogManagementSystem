import { Routes } from '@angular/router';

export const routes: Routes = [
    {
		path: 'dashboard',
		loadChildren: () =>
			import('./modules/dashboard/dashboard.module').then(
				m => m.DashboardModule,
			),
	}, 
	{
		path: 'auth',
		loadChildren: () =>
			import('./modules/auth/auth.module').then(
				m => m.AuthModule,
			),
	},
	{
		path: '',
		loadChildren: () =>
			import('./modules/main/main.module').then(
				m => m.MainModule,
			),
	},
];
