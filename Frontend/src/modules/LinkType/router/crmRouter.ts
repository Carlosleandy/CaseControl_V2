
import linktypeRepository from '../repository/linktypeRepository'

export default [    
    {
        path:'linktype',
        name:'linktype',
        component: () => import('../ui/linktype/LinkType.vue'),
        meta: {
            i18n: ['linktype']
        },
        props: {
            linktypeRepository,
        }
    },
];
