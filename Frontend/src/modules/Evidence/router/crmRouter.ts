
import evidenceRepository from '../repository/evidenceRepository'

export default [    
    {
        path:'evidence',
        name:'evidence',
        component: () => import('../ui/evidence/Evidence.vue'),
        meta: {
            i18n: ['evidence']
        },
        props: {
            evidenceRepository,
        }
    },
];
