
import faultlinkedRepository from '../repository/faultlinkedRepository'

export default [    
    {
        path:'faultlinked',
        name:'faultlinked',
        component: () => import('../ui/faultlinked/FaultLinked.vue'),
        meta: {
            i18n: ['faultlinked']
        },
        props: {
            faultlinkedRepository,
        }
    },
];
