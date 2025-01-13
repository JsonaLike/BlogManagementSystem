export const AdminSidebarItems = [
    {
        path: 'home',
        imageSrc: '/assets/home-outline.svg',
        title: 'Home',
        children: [
          { title: 'Overview', path: '/overview', imageSrc: '/assets/icons/overview.svg' },
          { title: 'Reports', path: 'reports', imageSrc: '/assets/icons/reports.svg' }
        ],
        expanded:false
    },
    {
        path: '/dashboard/posts',
        imageSrc: '/assets/post.png',
        title: 'Posts',
        children: [],
    },
    {
        path: '/dashboard/categories',
        imageSrc: '/assets/category.svg',
        title: 'Categories',
    },
    {
        path: '/dashboard/settings',
        imageSrc: '/assets/gear.png',
        title: 'Settings',
    },
    {
        path: '/help',
        imageSrc: '/assets/icons/help.png',
        title: 'Help',
    },
];
